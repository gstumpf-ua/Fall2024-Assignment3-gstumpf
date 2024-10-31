// Script to add interactivity and styling effects
document.addEventListener('DOMContentLoaded', function () {
    const movieCards = document.querySelectorAll('.movie-card');

    // Add a "fade-in" effect on load
    movieCards.forEach((card, index) => {
        setTimeout(() => {
            card.classList.add('fade-in');
        }, index * 150);
    });

    // Add an interactive color change on hover
    movieCards.forEach(card => {
        card.addEventListener('mouseenter', () => {
            card.style.backgroundColor = `hsl(${Math.floor(Math.random() * 360)}, 100%, 85%)`;
        });

        card.addEventListener('mouseleave', () => {
            card.style.backgroundColor = '#89CFF0';
        });
    });

});
