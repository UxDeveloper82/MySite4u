//Sticky navigation
//Get Values
const services = document.querySelector('.partners');
const initialCoords = services.getBoundingClientRect();
const nav = document.querySelector('.navbar');

window.addEventListener('scroll', function () {
    if (window.scrollY > initialCoords.top)
        nav.classList.add('sticky');
    else
        nav.classList.remove('sticky');
});