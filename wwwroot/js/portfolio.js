
// Declare Values
const baseURL = '/api/portfolio';
const sectionCard = document.querySelector('#card');
const searchBar = document.getElementById('searchBar');
const btncontainer = document.querySelector('#btn-container');
let portsCharacters;
let index = 0
let pages = []

const setupUI = () => {
    displayMenuItems(pages[index])
    displayButtons(btncontainer, pages, index)
}

const init = async () => {
    const portfolios = await loadPorts();
    pages = paginate(portfolios);
    setupUI();
}

btncontainer.addEventListener('click', function (e) {
    if (e.target.classList.contains('btn-container')) return
    if (e.target.classList.contains('page-btn')) {
        index = parseInt(e.target.dataset.index)
    }
    if (e.target.classList.contains('next-btn')) {
        index++
        if (index > pages.length - 1) {
            index = 0
        }
    }
    if (e.target.classList.contains('prev-btn')) {
        index--
        if (index < 0) {
            index = pages.length - 1
        }
    }
    setupUI()
})


//Search Function
searchBar.addEventListener('keyup', (e) => {
    const searchString = e.target.value.toLowerCase();
    const filteredPorts = portsCharacters.filter(portfolios => {
        return portfolios.name.toLowerCase().includes(searchString) ||
            portfolios.type.toLowerCase().includes(searchString);
    });
    displayMenuItems(filteredPorts);
});

// Fetch the items 
const loadPorts = async () => {
    try {
        const res = await fetch(baseURL);
        portsCharacters = await res.json();
        if (!res.ok) throw new Error(`${portsCharacters.message} (${res.status})`);
        return portsCharacters;
    } catch (err) {
        console.log(err);
    }
};
loadPorts();


//Display items to screen
const displayMenuItems = (portfolios) => {
    const sortPort = portfolios.sort((a, b) => (a.created < b.created) ? 1 : -1);
    const htmlString = sortPort.map((portfolios) => {
        return ` 
               <div class="col-lg-4 item ${portfolios.type}">
                            <div class="card">
                                <div class="card-head">
                                    <img src="/content/blog/${portfolios.portfolioPhoto}" alt="" class="img-fluid card-img">
                                    <div class="card-hover">
                                        <h4>Details: ${portfolios.type}</h4>
                                    </div>
                                </div>
                                <div class="card-body text-center">
                                    <h4 class="title">${portfolios.name}</h4>
                                    <a href="/UI/Portfolio/details/${portfolios.id}" class="btn btn-lg card-btn">More Details</a>
                                </div>
                            </div>
                    </div>
               `;
    }).join('');
    if (sortPort.length < 1) {
        let sectionCard = document.querySelector("#card");
        sectionCard.innerHTML = "<h2>Sorry, no products matched your search</h2>";
    } else {
        sectionCard.innerHTML = htmlString;
    }
    loadCard();
};

//Filter Function
function displayMenuButtons() {
    const filterBtns = document.querySelectorAll('.filter-btn');
    //filter items
    filterBtns.forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            const type = e.currentTarget.dataset.id;
            const menuType = portsCharacters.filter(function (port) {
                console.log(port.type);
                if (port.type === type) {
                    return port;
                }
            });
            if (type === 'all') {
                displayMenuItems(portsCharacters);
            } else {
                displayMenuItems(menuType);
            }
        })
    })
}
displayMenuButtons();

// Show CardHover
function loadCard() {
    $('.card').mouseenter(function () {
        $(this).find('.card-overlay').css({ 'top': '-100%' });
        $(this).find('.card-hover').css({ 'top': '0' });
    }).mouseleave(function () {
        $(this).find('.card-overlay').css({ 'top': '0' });
        $(this).find('.card-hover').css({ 'top': '100%' });
    });
};
loadPorts();
const paginate = (portfolios) => {
    const itemsPerPage = 10;
    const numberOfPages = Math.ceil(portfolios.length / itemsPerPage);

    const newPortfolios = Array.from({ length: numberOfPages }, (_, index) => {
        const start = index * itemsPerPage
        return portfolios.slice(start, start + itemsPerPage)
    });
    return newPortfolios
}


const displayButtons = (btncontainer, pages, activeIndex) => {
    let btns = pages.map((_, pageIndex) => {
        return `<button class="page-btn 
                ${activeIndex === pageIndex ? 'active-btn' : 'null '
                }" data-index="${pageIndex}">
                  ${pageIndex + 1}
                  </button>`
    })
    btns.push(`<button class="next-btn">next</button>`)
    btns.unshift(`<button class="prev-btn">prev</button>`)
    btncontainer.innerHTML = btns.join('')
}
displayButtons(btncontainer, pages, index);



//load items
window.addEventListener('load', init);
