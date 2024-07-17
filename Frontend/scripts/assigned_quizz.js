
function callMethod(){

    var customQuizzLiveContainer = document.querySelector('.custom-quizz-live-container');
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

    if(localStorage.getItem('customQuizzTestId') != null){
        customQuizzGenerator.style.display = "none";
        customQuizzLiveContainer.style.display = "block";
        ResumeTest(userEmail);
    }

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
        
        customQuizzLiveContainer.style.display = "block";
        GetChoiceFromUserForQuizz(userEmail,userRole,userId);
        
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


function GetChoiceFromUserForQuizz(userEmail){
    var customQuizzLiveContainer = document.querySelector('.custom-quizz-live-container');
    var buttonLoad = document.querySelector('.loadTestButton');
    buttonLoad.style.display = "none";
    DisplayAssignedQuizz(userEmail);
    // Add event listener to the form's submit event
}


function DisplayAssignedQuizz(userEmail){

    var customQuizzLiveContainer = document.querySelector('.custom-quizz-live-container');



    customQuizzLiveContainer.style.display = "block";

    // const timerContainer = document.querySelector('.timer-container');
    // let countdown = 5;

    // function updateCountdown() {
    //     timerContainer.textContent = `Redirecting to home in ${countdown} seconds...`;
    //     if (countdown === 0) {
    //         LiveQuizz(data);
    //     } else {
    //         countdown--;
    //         setTimeout(updateCountdown, 1000); 
    //     }
    // }

    // updateCountdown();


    
    // localStorage.setItem('customQuizzAssignmentNumber',AssignmentNumber);
    ResumeTest(userEmail);

}