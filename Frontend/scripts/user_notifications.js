const headers = [
    "Serial Number",
    "Assigned By",
    "Assignment Number",
    "Test ID",
    "Test Name",
    "Question Count",
    "Test Duration",
    "Test Time Window Open",
    "Test Time Window Close",
    "Test Status",
    "Attempt Allowed",
    "Attempt Count",
    "Last Attempt Date",
    "Take Test",
    "Test Results and Key"
  ];
  
  let notificationArray = [];
  let currentSortColumn = '';
  let isAscending = true;
  
  document.addEventListener('DOMContentLoaded', function () {
    const token = localStorage.getItem('token');
    if (!token) {
      window.location.href = '../user_login.html';
      return;
    }
  
    try {
      var tokenDecoded = jwt_decode(token);
    } catch (error) {
      console.error('Error decoding token:', error);
      window.location.href = '../user_login.html';
      return;
    }
  
    const userEmail = tokenDecoded.sub;
    const userRole = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (isTokenExpired()) {
      handleTokenExpired();
      return;
  }

    fetchNotifications(userEmail, userRole);
  
    const headerRow = document.querySelector('.notification-header');
    headerRow.addEventListener('click', function(e) {
      if (e.target.tagName === 'TH') {
        const columnIndex = Array.from(headerRow.querySelectorAll('th')).indexOf(e.target); 
        const columnName = mapColumnIndexToField(columnIndex);
        
        if (columnName) {
          sortTable(columnName);
        }
      }
    });
  
    const searchInput = document.querySelector('#searchInput');
    searchInput.addEventListener('input', function () {
      const searchText = searchInput.value.toLowerCase();
      const filteredNotifications = notificationArray.filter(notification => {
        return notification.testName.toLowerCase().includes(searchText);
      });
      displayNotifications(filteredNotifications);
    });
  });
  

  function fetchNotifications(userEmail, userRole) {
    console.log('Fetching notifications for:', userEmail, userRole);
    fetch(`http://localhost:5246/api/Notification/TestNotification?email=${encodeURIComponent(userEmail)}&role=${encodeURIComponent(userRole)}`, {
      method: "GET",
      headers: {
        "content-type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem('token')}`
      },
    })
      .then(res => {
        if (!res.ok) {
          document.querySelector('.information').textContent = 'Failed to fetch notifications. Please try again later.';
        }
        return res.json();
      })
      .then(data => {
        if (data && data.length > 0) {
          notificationArray = data;
          displayNotifications(notificationArray);
        } else {
          document.querySelector('.information').textContent = 'No notifications found.';
          document.querySelector('.notification-table').style.display = 'none';
          document.querySelector('.filter-notifications').style.display = 'none';
        }
      })
      .catch(error => {
       
        document.querySelector('.information').textContent = 'Failed to fetch notifications. Please try again later.';
        document.querySelector('.notification-table').style.display = 'none';
        document.querySelector('.filter-notifications').style.display = 'none';
      });
  }
  
// ... (rest of the code)

function sortTable(columnName) {

    if (columnName === currentSortColumn) {
      isAscending = !isAscending;
    } else {
      currentSortColumn = columnName;
      isAscending = true;
    }
  
    notificationArray.sort((a, b) => {
      let valueA = a[columnName];
      let valueB = b[columnName];
      if (typeof valueA === 'string') {
        return isAscending ? valueA.localeCompare(valueB) : valueB.localeCompare(valueA);
      } else {
        return isAscending ? valueA - valueB : valueB - valueA;
      }
    });
    // console.log('Sorted:', notificationArray);
    // Re-render the table with the sorted data
    displayNotifications(notificationArray);
  }
  

  
  function mapColumnIndexToField(index) {
    const fields = [
      'serialNumber',
      'testAssignedBy',
      'assignmentNumber',
      'testID',
      'testName',
      'questionCount',
      'testDuration',
      'testWindowOpen',
      'testWindowClose',
      'testStatus',
      'attemptAllowed',
      'attemptCount',
      'lastAttemptDate',
      'GetAnswerkey',
      'TakeTest'
    ];
    return fields[index] || null; 
  }
  
  function jwt_decode(token) {
    let base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  
    return JSON.parse(jsonPayload);
  }
  
  function displayNotifications(data) {
    const tableBody = document.querySelector('.notification-table tbody');
    tableBody.innerHTML = ''; // Clear existing content
  
    data.forEach((notification, index) => {
      const row = document.createElement('tr');
      const startDateWindow = new Date(notification.testWindowOpen).toLocaleString();
      const endDateWindow = new Date(notification.testWindowClose).toLocaleString();
      const testStatus = notification.testStatus || "Unattempted";
      const attemptCount = notification.attemptCount || 0;
      const attemptAllowed = notification.attemptAllowed || "Unlimited";
      const lastAttemptDate = notification.lastAttemptDate ? new Date(notification.lastAttemptDate).toLocaleString() : "Not Attempted";
  
      // Create Take Test button cell
      const takeTestCell = document.createElement('td');
      const takeTestButton = document.createElement('button');
      takeTestButton.textContent = 'Take Test';
      takeTestButton.classList.add('take-test');
      takeTestButton.dataset.testId = notification.testID;
      takeTestButton.dataset.assignmentNumber = notification.assignmentNumber;
  
      takeTestButton.addEventListener('click', function() {
          localStorage.setItem('customQuizzAssignmentNumber', this.dataset.assignmentNumber);
          window.location.href = '../Users/assigned_quizz.html'; 
      });
  

  
      // Add other cells to the row 
      row.innerHTML += `
        <td>${index + 1}</td>
        <td>${notification.testAssignedBy}</td>
        <td>${notification.assignmentNumber}</td>
        <td>${notification.testID}</td>
        <td>${notification.testName}</td>
        <td>${notification.questionCount}</td>
        <td>${notification.testDuration}</td>
        <td>${startDateWindow}</td>
        <td>${endDateWindow}</td>
        <td>${testStatus}</td>
        <td>${attemptAllowed}</td>
        <td>${attemptCount}</td>
        <td>${lastAttemptDate}</td>        
        <td><button class="test-results" data-test-id="${notification.testID}">Test Results and Key</button></td>
      `; 

      tableBody.appendChild(row);
      takeTestCell.appendChild(takeTestButton);
      row.appendChild(takeTestCell);
    });
  }

  function isTokenExpired() {
    const token = localStorage.getItem('token');
    if (!token) {
        return true;
    }
    try {
        const tokenDecoded = jwt_decode(token);
        // Token expiration time in seconds
        const exp = tokenDecoded.exp;
        // Current time in seconds
        const now = Math.floor(Date.now() / 1000);
        // Check if token is expired
        return now >= exp;
    } catch (error) {
        console.error('Error decoding token:', error);
        return true;
    }
}
function handleTokenExpired() {
  console.log('Token expired');
  localStorage.removeItem('token');
  window.location.href = '../user_login.html'; // Redirect to login page
}