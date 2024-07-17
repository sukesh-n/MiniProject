// manage_quizz.js
document.addEventListener('DOMContentLoaded', async function() {
    const token = localStorage.getItem('token');
    if (!token) {
        console.error('Token not found in local storage');
        return;
    }

    let tokenDecoded;
    try {
        tokenDecoded = jwt_decode(token);
    } catch (error) {
        console.error('Error decoding JWT:', error);
        return;
    }
    const userEmailAddress = tokenDecoded.sub;
    const userId = tokenDecoded.userid;
    const tableContainer = document.querySelector('.my_assigned_quizzes');
    const apiEndpoint = `http://localhost:5246/api/Organizer/GetAllTestsByOrganizer/${userId}`;

    async function fetchTestsByOrganizer() {
        console.log('Fetching tests by organizer...');
        try {
            const response = await fetch(apiEndpoint, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const data = await response.json();
            createTestTable(data);
        } catch (error) {
            console.error('Fetch error:', error);
            tableContainer.innerHTML = `
                <p class="error">Error fetching tests: ${error.message}</p>
            `;
        }
    }

    function createTestTable(testData) {
        if (testData.length === 0) {
          tableContainer.innerHTML = `
                    <p class="success">No tests assigned by you!</p>
                `;
          return;
        }
    
        const tableHtml = `
                <h2 class="heading">Assigned Tests</h2>
                <table id="testTable"> 
                <thead>
                    <tr>
                    <th data-sort="number">Assignment No</th> 
                    
                    <th data-sort="string">Test Name</th>
                    <th data-sort="date">Start Time</th>
                    <th data-sort="date">End Time</th>
                    <th data-sort="number">Test Duration</th>
                    </tr>
                </thead>
                <tbody>
                    ${testData.map(test => `
                    <tr>
                        <td class="assignment-no">${test.assignmentNo}</td>
                        
                        <td>
                            <a href="../Organizer/test_details.html?testName=${encodeURIComponent(test.assignmentNo)}" target="_blank" class="test_detail_link">
                                ${test.testName}
                            </a>
                        </td>


                        <td>${new Date(test.startTimeWindow).toLocaleString()}</td>
                        <td>${new Date(test.endTimeWindow).toLocaleString()}</td>
                        <td>${test.testDuration} minutes</td>
                    </tr>
                    `).join('')}
                </tbody>
                </table>
            `;
        tableContainer.innerHTML = tableHtml;
    
        makeTableSortable('testTable');
      }
    function makeTableSortable(tableId) {
        const table = document.getElementById(tableId);
        const headers = table.querySelectorAll('th');
        const tbody = table.querySelector('tbody');
      
        headers.forEach(header => {
          header.addEventListener('click', () => {
            const sortType = header.dataset.sort;
            const sortDirection = header.classList.contains('asc') ? -1 : 1;
      
            const sortedRows = Array.from(tbody.querySelectorAll('tr')).sort((rowA, rowB) => {
              const cellA = rowA.querySelectorAll('td')[Array.from(headers).indexOf(header)].textContent.trim();
              const cellB = rowB.querySelectorAll('td')[Array.from(headers).indexOf(header)].textContent.trim();
      
              if (sortType === 'number') {
                return (parseFloat(cellA) - parseFloat(cellB)) * sortDirection;
              } else if (sortType === 'date') {
                // Correct date sorting 
                const [dateA, timeA] = cellA.split(', '); // Split date and time
                const [dateB, timeB] = cellB.split(', '); 
                const [monthA, dayA, yearA] = dateA.split('/').map(Number);
                const [monthB, dayB, yearB] = dateB.split('/').map(Number);
      
                const dateObjA = new Date(yearA, monthA - 1, dayA); // Month is 0-indexed
                const dateObjB = new Date(yearB, monthB - 1, dayB);
                return (dateObjA - dateObjB) * sortDirection;
              } else {
                return cellA.localeCompare(cellB) * sortDirection;
              }
            });
      
            sortedRows.forEach(row => tbody.appendChild(row));
      
            header.classList.toggle('asc', sortDirection === 1);
            header.classList.toggle('desc', sortDirection !== 1);
          });
        });
      }
      

    function jwt_decode(token) {
        let base64Url = token.split('.')[1];
        let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
      
        return JSON.parse(jsonPayload);
      }

    fetchTestsByOrganizer();
});