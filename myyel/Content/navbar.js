const navShow = () => {
    const burger = document.querySelector('.burger2');
    const nav = document.querySelector('.nav-links2');
    const nav_bg = document.querySelector('.menu-fixed');

    window.addEventListener('load', () => {
        nav_bg.classList.toggle('nav-active-2');
        nav_bg.classList.toggle('menu-bg');
    });

    burger.addEventListener('click', () => {
        nav_bg.classList.toggle('nav-active-2');
        nav_bg.classList.toggle('menu-bg');
    });
}


navShow();