
document.querySelector("#menu-toggle").addEventListener('click', function (e) {
    e.preventDefault();
    var wrapper = document.querySelector('#wrapper');
    wrapper.classList.toggle('toggled');
});



/*$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});*/
