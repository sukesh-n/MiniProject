function user_logout() {
    localStorage.removeItem('token');
    console.log('Token removed');

    window.location.href = './logout.html';
}
if (window.location.href.includes('logout.html')) {
    document.addEventListener('DOMContentLoaded', function() {
        const timerElement = document.querySelector('.timer');
        const redirectLink = document.querySelector('.redirect-link');
        let seconds = 5;
        timerElement.textContent = `Logging out in ${seconds} seconds`;
        redirectLink.innerHTML = '<a href="../user_login.html">Click here</a> to redirect to login page';
        const countdown = setInterval(() => {
            seconds--;
            timerElement.textContent = `Logging out in ${seconds} seconds`;

            if (seconds === 0) {
                clearInterval(countdown);
                console.log('Redirecting to login page');
                window.location.href = '../user_login.html';
            }
        }, 1000);
    });
}

