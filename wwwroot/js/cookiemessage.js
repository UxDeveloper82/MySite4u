//Creatting the cookie area
const body = document.querySelector('.cookie-message');
const message = document.createElement("div");
message.classList.add('.cookie-message');
message.innerHTML = 'We use cookied for improved functionally and analytics. <button class="btn btn-primary btn--close-cookie">Got it!</button>'
body.append(message);

//Delete elements
document.querySelector('.btn--close-cookie')
    .addEventListener('click', function () {
        //message.remove();
        message.parentElement.removeChild(message);
    });
//styles 
message.style.backgroundColor = '#4c4c4c';
message.style.width = '103%';