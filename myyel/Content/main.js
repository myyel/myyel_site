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


const sliderNavShow = () => {
    const navg = document.getElementById('0');
    const navg1 = document.getElementById('1');
    const navg2 = document.getElementById('2');
    const navg3 = document.getElementById('3');
    const navg4 = document.getElementById('4');
    const navg5 = document.getElementById('5');

    if (navg.classList.contains('active')) {
        navg3.classList.toggle('slider-nav-item-active');
    }
    navg.addEventListener('transitionstart', () => {
        if (navg.classList.contains('active')) {
            navg3.classList.toggle('slider-nav-item-active');
        }
        else {
            navg3.classList.remove('slider-nav-item-active');
        }
    });
    navg1.addEventListener('transitionstart', () => {
        if (navg1.classList.contains('active')) {
            navg4.classList.toggle('slider-nav-item-active');
        }
        else {
            navg4.classList.remove('slider-nav-item-active');
        }
    }); 
    navg2.addEventListener('transitionstart', () => {
        if (navg2.classList.contains('active')) {
            navg5.classList.toggle('slider-nav-item-active');
        }
        else {
            navg5.classList.remove('slider-nav-item-active');
        }
    });
}

sliderNavShow();

