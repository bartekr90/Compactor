
document.title = 'Nowy tytuł -Javascript';

let links = document.getElementsByClassName('link-block');

links[0].style.fontSize = '40px';

for (let link of links)
    link.style.fontSize = '40px';

function run() {
    updateHeader();
}

function updateHeader() {
    let header = document.getElementById('main-header');
    header.innerHTML = "Zmieniono tytuł na js"
}

$(document).ready(function () {
    $('#main-header').prop('innerHTML', 'jQuery 123');
});


