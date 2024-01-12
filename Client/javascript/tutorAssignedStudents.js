document.addEventListener('DOMContentLoaded', function () {
    const urlParams = new URLSearchParams(window.location.search);
    const tutorId = urlParams.get('tutorId');

    if (tutorId) {
        fetch(`http://localhost:5052/api/tutors/${tutorId}/assignedStudents`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to fetch students');
                }
                return response.json();
            })
            .then(students => {
                const studentsList = document.getElementById('studentsList');
                students.forEach(student => {
                    const studentElement = document.createElement('div');
                    studentElement.textContent = `${student.firstName} ${student.lastName} - ${student.email}`;
                    studentsList.appendChild(studentElement);
                });
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});
// Add event listeners to the navigation menu items (li elements)
const homeLink = document.getElementById('home');
homeLink.addEventListener('click', function () {


    // Redirect to the "home.html" page
    window.location.href = '/Client/views/home.html';
});