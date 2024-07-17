document.addEventListener('DOMContentLoaded', function() {
    const token = localStorage.getItem('token');
    const admin_login = document.querySelector('.admin_login');
    const candidate_login = document.querySelector('.candidate_login');
    const organizer_login = document.querySelector('.organizer_login');
    var tokenDecoded = jwt_decode(token);
    console.log(tokenDecoded);
    const userEmail = tokenDecoded.sub;
    const userRole = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(userRole.toLowerCase().trim() === 'admin'){
        admin_login.textContent = "Go to Admin Page";
        admin_login.style.color = "lightcoral";
    }
    else if(userRole.toLowerCase().trim() === 'candidate'){
        candidate_login.textContent = "Go to Candidate Page";
        candidate_login.style.color = "lightblue";
    }
    else if(userRole.toLowerCase().trim() === 'organizer'){
        organizer_login.textContent = "Go to Organizer Page";
        organizer_login.style.color = "lightgreen";
    }
    var loginInfo = document.querySelector('.login-info');
    if(loginInfo){
        loginInfo.textContent = `Logged in as ${userEmail} (${userRole})`;
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