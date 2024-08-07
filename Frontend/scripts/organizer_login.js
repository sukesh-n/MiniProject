
document.addEventListener('DOMContentLoaded', function(){
    const token = localStorage.getItem('token');

    if (token) {
        console.log('Token found');
        tokenDecoded = jwt_decode(token);
        const userRole = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        if (userRole.toLowerCase().trim() === 'organizer') {
            window.location.href = './Organizer/organizer_home.html';
        }        
    }
    const loginForm = document.querySelector('.login-form');
    const errorContainer = document.createElement('div');
    errorContainer.classList.add('error-message');

    const inputBox = document.querySelector('.input-box:focus');
    loginForm.appendChild(errorContainer);

    const usernameInput = loginForm.elements.namedItem('username');
    const passwordInput = loginForm.elements.namedItem('password');
    
    usernameInput.addEventListener('input', function() {
        if(usernameInput.value.trim() === '' && passwordInput.value.trim() === '') {
            errorContainer.textContent = 'Username and password cannot be empty';
        }
        if (usernameInput.value.trim() === '') {
            usernameInput.classList.add('input-error');
            usernameInput.classList.remove('input-success');            
            errorContainer.textContent = 'Username cannot be empty';
        } else if (usernameInput.value.length < 3) {
            usernameInput.classList.add('input-error');
            usernameInput.classList.remove('input-success');
            errorContainer.textContent = 'Username must be at least 3 characters';
        } else {
            usernameInput.classList.remove('input-error');
            usernameInput.classList.add('input-success');
            errorContainer.textContent = '';
        }
        
    });

    passwordInput.addEventListener('input', function() {
        if (passwordInput.value.trim() === '') {
            passwordInput.classList.add('input-error');
            passwordInput.classList.remove('input-success');
            if(usernameInput.value.trim() === '') {
                errorContainer.textContent = 'Username and password cannot be empty';
            }
            else{
                errorContainer.textContent = 'Password cannot be empty';
            }
            // errorContainer.textContent = 'Password cannot be empty';
        } else if (passwordInput.value.length < 3) {
            passwordInput.classList.add('input-error');
            passwordInput.classList.remove('input-success');
            errorContainer.textContent = 'Password must be at least 3 characters';
        } else {
            passwordInput.classList.remove('input-error');
            passwordInput.classList.add('input-success');
            errorContainer.textContent = '';
        }

    });

    loginForm.addEventListener('submit', function(e) {
        e.preventDefault();
    
        const userName = usernameInput.value.trim();
        const password = passwordInput.value.trim();
        let errorMessage = '';
    
        if (userName === '') {
            usernameInput.classList.add('input-error');
            errorMessage = 'Username cannot be empty';
            
        } else if (userName.length < 3) {
            usernameInput.classList.add('input-error');
            errorMessage = 'Username must be at least 3 characters';
        } else {
            usernameInput.classList.remove('input-error');
        }
    
        if (password === '') {
            passwordInput.classList.add('input-error');
            errorMessage = 'Password cannot be empty';
        } else if (password.length < 3) {
            passwordInput.classList.add('input-error');
            errorMessage = 'Password must be at least 3 characters';
        } else {
            passwordInput.classList.remove('input-error');
        }
        if(userName === '' && password === ''){
            errorMessage = 'Username and password cannot be empty';
        }
        errorContainer.textContent = errorMessage;
    });
    
})
function OrganizerLogin() {

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    if(username === '' || password === ''){
        return;
    }
    const login = document.getElementById("login");
    const errorContainer = document.querySelector('.error-message');
    
    const token = fetch("http://localhost:5246/api/Login/OrganizerLogin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email: username,
            password: password,
        }),
    })
    .then((res) => {
        if (!res.ok) {
            errorContainer.textContent = 'Login failed. Please try again with correct username and password.';
        }
        return res.json();
    })
    .then((data) => {
        console.log(data);
        if (data.token) {         
            localStorage.clear();
            localStorage.setItem("token", data.token);
            window.location.href = "./organizer/organizer_home.html";
        } else {
            console.log('Login failed.');
            if (data.message) {
                errorContainer.textContent = `Login failed: ${data.message}`;
            } else {
                errorContainer.textContent = 'Login failed. Please try again.';
            }
        }
    });
}
// document.querySelector('.login-form').addEventListener('submit', function(e) {
//     e.preventDefault();
//     UserLogin();
// });
function jwt_decode(token) {
    let base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    let jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}