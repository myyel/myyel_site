const RegisterShow = () => {
    const burger = document.querySelector('.burger2');
    const registerForm = document.querySelector('.register-bg');
    const register = document.querySelector('.menu-btn-2');
    const loginForm = document.querySelector('.login-bg');

    if (register != null) {

        register.addEventListener('click', () => {
            if (registerForm.classList.contains('register-bg-active')) {
                registerForm.classList.remove('register-bg-active');
            } else {
                registerForm.classList.toggle('register-bg-active');
            }
            if (loginForm.classList.contains('login-bg-active')) {
                loginForm.classList.remove('login-bg-active');
            }
        });

    }

    burger.addEventListener('click', () => {
        if (registerForm.classList.contains('register-bg-active')) {
            registerForm.classList.remove('register-bg-active');
        }
    });
}

RegisterShow();

const LoginShow = () => {
    const burger2 = document.querySelector('.burger2');
    const loginForm = document.querySelector('.login-bg');
    const login = document.querySelector('.menu-btn');
    const registerForm = document.querySelector('.register-bg');

    if (login != null) {
        login.addEventListener('click', () => {
            if (loginForm.classList.contains('login-bg-active')) {
                loginForm.classList.remove('login-bg-active');
            } else {
                loginForm.classList.toggle('login-bg-active');
            }
            if (registerForm.classList.contains('register-bg-active')) {
                registerForm.classList.remove('register-bg-active');
            }
        });
    }


    burger2.addEventListener('click', () => {
        if (loginForm.classList.contains('login-bg-active')) {
            loginForm.classList.remove('login-bg-active');
        }
    });
}

LoginShow();