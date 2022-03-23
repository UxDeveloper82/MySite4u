let portCharacters = [];
const portList = document.getElementById('ports');
const portfolioId = document.getElementById("Id").value;

const loadPortsDetails = async () => {
    try {
        const res = await fetch(`/api/Portfolio/${portfolioId}`);
        portCharacters = await res.json();
        displayPorts(portCharacters);
    }
    catch (err) {
        console.log(err);
    }
};
loadPortsDetails();
const displayPorts = (port) => {
    const htmlString = `
                 <img style="width:100%" class="img-thumbnail" src="/Content/Blog/${port.portfolioPhoto}" />
                 <h4 class="title">Name: ${port.name}</h4>
                <h4 class="about-title">Type: ${port.type}</h4>
                <h4 class="about">Details: ${port.details} ...</h4>
                <h4 class="about">Language: ${port.language}</h4>
                <a class="btn btn-outline-warning" href="${port.codeLink}">Github link</a>
                <a class="btn btn-outline-info" href="${port.link}">Goto Page</a>`;
    portList.innerHTML = htmlString;
};
