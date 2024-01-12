document.addEventListener('DOMContentLoaded', function () {
    const apiUrl = 'http://localhost:5052/api/statics';



    fetch(apiUrl).then(response => {
        if (!response.ok) {
            throw new Error('Failed to fetch statistics');
        }
        return response.json();
    })
        .then(statistics => {
            document.getElementById('numStudents').innerText = statistics.TotalStudents;
            document.getElementById('numTutors').innerText = statistics.TotalTutors;
            document.getElementById('numCourses').innerText = statistics.TotalSubjects;



            // Add event listeners to the buttons
            document.getElementById('tutorButton').addEventListener('click', function () {
                // Redirect to tutor.html
                window.location.href = '/Client/views/tutorRegistration.html';
                console.log(window.location.href);
            });

            document.getElementById('studentButton').addEventListener('click', function () {
                // Redirect to student.html
                window.location.href = '/Client/views/studentRegistration.html';
            });


        })
        .catch(error => {
            console.error('Error:', error);
        })
});