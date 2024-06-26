document.addEventListener('DOMContentLoaded', function() {
    const token = localStorage.getItem('token');
    if (!token) {
        window.location.href = '../user_login.html';
        return;
    }

    let tokenDecoded;
    try {
        tokenDecoded = jwt_decode(token);
    } catch (error) {
        console.error('Error decoding token:', error);
        window.location.href = '../user_login.html';
        return;
    }

    const userEmail = tokenDecoded.sub;
    const userRole = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    const userId = tokenDecoded.userid;

    const errorMessage = document.querySelector('.error-message');
    const loginInfo = document.querySelector('.login-info');
    const timer = document.querySelector('.timer');

    loginInfo.textContent = `Logged in as ${userEmail} (${userRole})`;

    if (userRole.toLowerCase().trim() !== 'candidate') {
        errorMessage.textContent = `You are not authorized to view this page. Your role is ${userRole}. Please login as a candidate to view this page Or Register as a candidate if you are not one.`;
        errorMessage.style.display = "block";
        
        let countdown = 5;
        function updateCountdown() {
            timer.textContent = `Redirecting to home in ${countdown} seconds...`;
            if (countdown === 0) {
                window.location.href = '../user_login.html';
            } else {
                countdown--;
                setTimeout(updateCountdown, 1000); 
            }
        }
        updateCountdown();
    } else {
        errorMessage.style.display = "none";
        timer.style.display = "none";
        GetChoiceFromUserForQuizz(userEmail,userRole,userId);
    }
});

function jwt_decode(token) {
    let base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  
    return JSON.parse(jsonPayload);
  }

var choiceDTO = {
    "noOfQuestions": 0,
    "mainCategory": "string",
    "subCategory": "string",
    "difficultyLevel": 0,
    "type": "string"
  }

function GetChoiceFromUserForQuizz(userEmail,userRole,userId){
    var customQuizzGenerator = document.querySelector('.custom-quizz-generator');
    customQuizzGenerator.style.display = "block";

    // Get the form elements for the custom quiz generator
    const quizzForm = document.querySelector('.quizz-form');
    const noOfQuestionsInput = quizzForm.querySelector('#noOfQuestions');
    const mainCategorySelect = quizzForm.querySelector('#mainCategory');
    const subCategorySelect = quizzForm.querySelector('#subCategory');
    const difficultyLevelSelect = quizzForm.querySelector('#difficultyLevel');
    const typeSelect = quizzForm.querySelector('#type');

    // Add event listener to the form's submit event
    quizzForm.addEventListener('submit', (event) => {
        event.preventDefault();
        // Populate the choiceDTO object
        choiceDTO = {
            "noOfQuestions": parseInt(noOfQuestionsInput.value),
            "mainCategory": mainCategorySelect.value,
            "subCategory": subCategorySelect.value,
            "difficultyLevel": parseInt(difficultyLevelSelect.value),
            "type": typeSelect.value
        };

        fetch(`http://localhost:5246/api/Candidate/GetCustomQuizz/${userId}`,{
            method: "PUT",
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('token')}`,
            },
            body: JSON.stringify({
                choiceDTO
            }),

        })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Failed to get custom quizz');
            }
        })
        .then(data => {
            // Handle successful response
            console.log('Custom Quizz assigned successfully:', data);
            window.location.href = './quizz.html?userId=' + userId;
        })
        .catch(error => {
            // Handle errors
            console.error('Error getting custom quizz:', error);
            // Display an error message to the user
        });

    });

}