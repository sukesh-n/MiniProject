document.addEventListener('DOMContentLoaded', async () => {
  const token = localStorage.getItem('token');
  if (!token) {
    console.error('Token not found in local storage');
    return;
  }
  const tokenDecoded = jwt_decode(token);
  const urlParams = new URLSearchParams(window.location.search);
  const assignmentNumber = decodeURIComponent(urlParams.get('testName'));
  const detailsContainer = document.querySelector('.test_details'); 
  const currentUserId = tokenDecoded.userid;

  // Function to sort the table
  function sortTable(columnIndex, dataType) {
    const table = document.querySelector('table');
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));

    rows.sort((a, b) => {
      const cellA = a.querySelectorAll('td')[columnIndex].textContent.trim();
      const cellB = b.querySelectorAll('td')[columnIndex].textContent.trim();

      if (dataType === 'number') {
        return parseFloat(cellA) - parseFloat(cellB);
      } else {
        return cellA.localeCompare(cellB);
      }
    });

    tbody.innerHTML = ''; 
    rows.forEach(row => tbody.appendChild(row));
  }

  if (assignmentNumber) {
    detailsContainer.innerHTML = `<h2>Loading details for "${assignmentNumber}"...</h2>`;

    try {
      const apiEndpoint = `http://localhost:5246/api/Organizer/GetTestDetails/${currentUserId}/${assignmentNumber}`; 
      const response = await fetch(apiEndpoint, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` 
        }
      });

      if (!response.ok) {
        throw new Error(`API request failed with status ${response.status}`);
      }

      const testData = await response.json();
      console.log("Test Data:", testData); 

      // Create table HTML
      let tableHtml = `
        <h2>Test Details</h2>
        <table>
          <thead>
            <tr>
              <th onclick="sortTable(0, 'string')">Candidate Email</th>
              <th onclick="sortTable(1, 'string')">Is Admin</th>
              <th onclick="sortTable(2, 'string')">Is Candidate</th>
              <th onclick="sortTable(3, 'string')">Is Organizer</th>
              <th onclick="sortTable(4, 'string')">Test Status</th>
              <th onclick="sortTable(5, 'string')">Test Start Time</th>
              <th onclick="sortTable(6, 'string')">Test End Time</th>
            </tr>
          </thead>
          <tbody>
      `;

      testData.forEach(testDataItem => {
        const formattedStartTime = testDataItem.testStartTime 
          ? new Date(testDataItem.testStartTime).toLocaleString() 
          : 'N/A';

        const formattedEndTime = testDataItem.testEndTime 
          ? new Date(testDataItem.testEndTime).toLocaleString() 
          : 'N/A';

        tableHtml += `
          <tr>
            <td>${testDataItem.candidateEmails}</td>
            <td>${testDataItem.isAdmin}</td>
            <td>${testDataItem.isCandidate}</td>
            <td>${testDataItem.isOrganizer}</td>
            <td>${testDataItem.statusOfTest}</td>
            <td>${formattedStartTime}</td>
            <td>${formattedEndTime}</td>
          </tr>
        `;
      });

      tableHtml += `
          </tbody>
        </table>
      `;

      detailsContainer.innerHTML = tableHtml;

      // Add click event listeners for sorting AFTER table is in the DOM
      const tableHeaders = document.querySelectorAll('th');
      tableHeaders.forEach((header, index) => {
        header.addEventListener('click', () => {
          const dataType = index === 5 || index === 6 ? 'number' : 'string'; 
          sortTable(index, dataType);
        });
      });


    } catch (error) {
      console.error('Error fetching or displaying test details:', error);
      detailsContainer.innerHTML = `<p class="error">Error loading test details. ${error.message}</p>`; 
    }
  } else {
    console.error("Test name not found in URL parameters");
    detailsContainer.innerHTML = `<p class="error">Test name not found.</p>`;
  }
});


function jwt_decode(token) {
  let base64Url = token.split('.')[1];
  let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  let jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
  }).join(''));

  return JSON.parse(jsonPayload);
}