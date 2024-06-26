document.addEventListener('DOMContentLoaded', function() {
    const token = localStorage.getItem('token');
    if(token===null){
        window.location.href = '../user_login.html';
    }
    var tokenDecoded = jwt_decode(token);
    console.log(tokenDecoded);
    const userEmail = tokenDecoded.sub;
    const userRole = tokenDecoded.role;

    var loginInfo = document.querySelector('.login-info');
    loginInfo.textContent = `Logged in as ${userEmail} (${userRole})`;
});

function jwt_decode(token) {
    let base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}