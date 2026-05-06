// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('logout-form').addEventListener('click', function () {
    document.getElementById('logout-form').submit();
});


function shakeBell() {
    const bellIcon = document.querySelector('.not');

    bellIcon.classList.add('shake');

    setTimeout(() => {
        bellIcon.classList.remove('shake');
    }, 500);
}