document.addEventListener('DOMContentLoaded', function(){
    const loginForm = document.querySelector('.login-form');
    const errorContainer = document.createElement('div');
    errorContainer.classList.add('error-message');

    const inputBox = document.querySelector('.input-box:focus');
    loginForm.appendChild(errorContainer);

    const usernameInput = loginForm.elements.namedItem('username');
    const passwordInput = loginForm.elements.namedItem('password');

    usernameInput.addEventListener('input', function() {
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
            errorContainer.textContent = 'Password cannot be empty';
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
    
        errorContainer.textContent = errorMessage;
    });
    
})

function UserLogin() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const login = document.getElementById("login");
        const error = document.getElementById("error");
        const success = document.getElementById("success");

        fetch('http://localhost:5246/api/Login/AdminLogin', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: username, password: password })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('API Response:', data.token);
                console.log('API Response:', data.role);
                console.log('app',login);
                if (login) login.style.display = "none";

                if (data.success) {
                    console.log('Login successful!');
                    if (success) {
                        success.style.display = "block";
                        success.textContent = 'Login successful!';

                        if (data.message) {
                            success.textContent += ` Message: ${data.message}`;
                        }
                        if (data.token) {
                            success.textContent += ` Token: ${data.token}`;
                        }
                    }
                } else {
                    console.log('Login failed!');
                    if (error) {
                        error.style.display = "block";
                        error.textContent = 'Login failed.';
                        console.log('API Response:sskks', data.message);
                        if (data.message) {
                            error.textContent += ` Message: ${data.message}`;
                            console.log('API Response:', data.message);
                        }
                        if (data.errors) {
                            error.textContent += ` Errors: ${data.errors.join(', ')}`;
                            console.log('API Response:', data.errors);
                        }
                    }
                }
            })
            .catch(error => {
                console.error('Error:', error);
                if (login) login.style.display = "none";
                if (error) {
                    error.style.display = "block";
                    error.textContent = `An error occurred: ${error.message}`;
                }
            });
}
