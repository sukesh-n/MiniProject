document.addEventListener('DOMContentLoaded', function() {
    const apiEndpoint = 'http://localhost:5246/api/Organizer/AssignTest'; 
    const token = localStorage.getItem('token');
    let tokenDecoded = jwt_decode(token);
    const userEmail = tokenDecoded.sub;
    const userRole = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    const userId = tokenDecoded.userid;

    const formContainer = document.querySelector('.form-container');
    let allCategories = [];

    fetchCategories();

    function createForm() {
        const form = document.createElement('form');
        form.id = 'assign-quiz-form';
        form.innerHTML = `
            <h2>Question Selection</h2>
            <div class="form-group">
                <label for="noOfQuestions">Number of Questions:</label>
                <input type="number" id="noOfQuestions" name="noOfQuestions" min="1" required placeholder="Enter No Of Questions">
            </div>
            <div class="form-group">
                <label for="mainCategory">Main Category:</label>
                <select id="mainCategory" name="mainCategory" required>
                    <option value="">Select Category</option>
                </select>
            </div>
            <div class="form-group">
                <label for="subCategory">Sub Category:</label>
                <select id="subCategory" name="subCategory" required>
                    <option value="">Select Sub Category</option>
                </select>
            </div>
            <div class="form-group">
                <label for="difficultyLevel">Difficulty Level:</label>
                <select id="difficultyLevel" name="difficultyLevel" required>
                    <option value="">Select Difficulty Level</option>
                    <option value="0">Any</option> 
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                    <option value="9">9</option>
                    <option value="10">10</option>
                </select>
            </div>
            <div class="form-group">
                <label for="type">Type:</label>
                <select id="type" name="type" required>
                    <option value="">Select Type</option>
                    <option value="">Any</option> 
                    <option value="MCQ">MCQ</option>
                    <option value="True/False">True/False</option>
                    <option value="Fill in the blanks">Numerical Answers</option>
                </select>
            </div>

            <h2>Test Assignment</h2>
            <div>
            <label for="assignedBy">Assigned By(Organizer Id):</label>
            <input type="number" id="assignedBy" name="assignedBy" value="${userId}" readonly>
            </div>
            <div>
            <label for="assignedByEmail">Assigned By(Organizer Email):</label>
            <input type="text" id="assignedByEmail" name="assignedByEmail" value="${userEmail}" readonly>
            </div>
            <div>
            <label for="testName">Test Name:</label>
            <input type="text" id="testName" name="testName" placeholder="Enter Test Name">
            </div>
            <div>
            <label for="startTimeWindow">Start Time:</label>
            <input type="datetime-local" id="startTimeWindow" name="startTimeWindow">
            </div>
            <div>
            <label for="endTimeWindow">End Time:</label>
            <input type="datetime-local" id="endTimeWindow" name="endTimeWindow">
            </div>
            <div>
            <label for="testDuration">Test Duration (minutes):</label>
            <input type="number" id="testDuration" name="testDuration" min="1"  required placeholder="Enter Test Duration">
            </div>
            <div>
            <label for="candidateEmails">Candidate Emails (comma-separated):</label>
            <input type="text" id="candidateEmails" name="candidateEmails" placeholder="Example : abc@cba.ni,asd@ac.in">
            </div>

            <button type="submit">Assign Quiz</button>
        `;

        // Event listener for main category change
        const mainCategorySelect = form.querySelector('#mainCategory');
        mainCategorySelect.addEventListener('change', () => {
            loadSubCategory(mainCategorySelect.value);
        });

        form.addEventListener('submit', handleSubmit);
        formContainer.appendChild(form);

        populateMainCategories();
    }

    function populateMainCategories() {
        const mainCategorySelect = document.getElementById('mainCategory');
        allCategories.forEach(category => {
            const option = document.createElement('option');
            option.value = category.mainCategory;
            option.textContent = category.mainCategory;
            mainCategorySelect.appendChild(option);
        });
    }

    async function handleSubmit(event) {
        event.preventDefault();
        const formData = new FormData(event.target);

        // if (!validateForm(formData)) {
        //     return; 
        // }

        const startTime = new Date(formData.get('startTimeWindow'));
        const endTime = new Date(formData.get('endTimeWindow'));
        const currentTime = new Date();
        if(startTime < currentTime) {
            alert('Error: Start time must be in the future!');
            return;
        }
        if (startTime >= endTime) {
            alert('Error: Start time must be before end time!');
            return;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        const candidateEmails = formData.get('candidateEmails').split(',').map(email => email.trim());
        for (const email of candidateEmails) {
            if (!emailRegex.test(email)) {
                alert(`Error: Invalid email format: ${email}`);
                return;
            }
        }
        const TestAssignDTO = {
            questionSelectionDTO: {
            noOfQuestions: parseInt(formData.get('noOfQuestions')),
            mainCategory: formData.get('mainCategory'),
            subCategory: formData.get('subCategory'),
            difficultyLevel: parseInt(formData.get('difficultyLevel')),
            type: formData.get('type'),
            },
            testAssignDTO: {
            assignmentNo: 0,
            assignedBy: parseInt(formData.get('assignedBy')),
            testName: formData.get('testName'),
            startTimeWindow: formData.get('startTimeWindow'),
            endTimeWindow: formData.get('endTimeWindow'),
            testDuration: parseInt(formData.get('testDuration')),
            candidateEmails: formData.get('candidateEmails').split(',').map(email => email.trim()),
            },
        };
        console.log('TestAssignDTO:', TestAssignDTO);
        try {
            const response = await fetch(apiEndpoint, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}` 
            },
            body: JSON.stringify(TestAssignDTO),
            });

            if (response.ok) {
            const data = await response.json();
            event.target.remove();
            const tableContainer = document.createElement('div');
            tableContainer.classList.add("table-container"); 

            // Create HTML table from data
            const tableHtml = `
                <p class="success">Quiz assigned successfully!</p>
                <h3 class="success">Your Assignment Number is: ${data.assignmentNo}</h3>
                <table>
                <thead>
                    <tr>
                    <th>Property</th>
                    <th>Value</th>
                    </tr>
                </thead>
                <tbody>
                    ${Object.entries(data).map(([key, value]) => `
                    <tr>
                        <td>${key}</td>
                        <td>${Array.isArray(value) ? value.join(', ') : value}</td> 
                    </tr>
                    `).join('')}
                </tbody>
                </table>
                <a href="../Organizer/manage_quizz.html" class="assign_redirect">Go to Manage Quizzes</a>
            `;

            tableContainer.innerHTML = tableHtml;

            // Append the tableContainer to the formContainer
            formContainer.appendChild(tableContainer);
            }
            else {
            const errorData = await response.json();
            formContainer.innerHTML = `
                <p class="error">Error assigning quiz: ${errorData.message || 'Unknown error'}</p>
            `;
            }
        } catch (error) {
            console.error('Error:', error);
            formContainer.innerHTML = `
            <p class="error">Error assigning quiz: ${error.message || 'Network error'}</p>
            `;
        }
    }

    function loadSubCategory(mainCategory) {
        const subCategorySelect = document.getElementById('subCategory');
        subCategorySelect.innerHTML = '<option value="">Select Sub Category</option>'; 
        allCategories.forEach(sub => {
            if (sub.mainCategory === mainCategory) {
            const option = document.createElement('option');
            option.value = sub.subCategory;
            option.textContent = sub.subCategory;
            subCategorySelect.appendChild(option);
            }
        });
    }

    async function fetchCategories() {
        try {
            const response = await fetch('http://localhost:5246/api/Common/GetAllCategories', {
            method: "GET",
            headers: {
                "content-type": "application/json",
            }
            });

            if (response.ok) {
            allCategories = await response.json();
            createForm(); 
            } else {
            throw new Error('Failed to get categories');
            }
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    }

    function jwt_decode(token) {
        let base64Url = token.split('.')[1];
        let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        return JSON.parse(jsonPayload);
    }

    function validateForm(formData) {
        const requiredFields = [
            'noOfQuestions', 'mainCategory', 'subCategory', 'difficultyLevel', 
            'type', 'testName', 'startTimeWindow', 'endTimeWindow', 
            'testDuration', 'candidateEmails'
        ];

        for (const field of requiredFields) {
            if (!formData.get(field)) {
            alert(`Error: Please fill in the ${field} field.`);
            return false; 
            }
        }
        return true; 
    }

});