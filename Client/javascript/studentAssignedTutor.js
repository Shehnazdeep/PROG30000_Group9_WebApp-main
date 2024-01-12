document.addEventListener('DOMContentLoaded', function () {
    // Extract studentId from URL
    const urlParams = new URLSearchParams(window.location.search);
    const studentId = urlParams.get('studentId');
    console.log('studentId:', studentId);

    // Fetch assigned tutors data from the specific endpoint
    fetch(`http://localhost:5052/api/students/${studentId}/assignedTutors`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch assigned tutors data');
            }
            return response.json();

        })
        .then(data => {
            console.log('assignedTutors:', data);
            // Display assigned tutors in the HTML
            const assignedTutors = data.tutorsAssignedTothisStudentID;
            const studentInfo = data.student.firstName;
            const school = data.student.school;
            const tutorList = document.getElementById('tutorList');
            document.getElementById('studentName').textContent = ` Student Name: ${studentInfo}`;
            document.getElementById('studentSchool').textContent = ` School: ${school}`;
            document.getElementById('desc').textContent = `My Tutors`;
            let tutorNumber = 1; // Initialize tutor number
            assignedTutors.forEach(tutor => {
                const tutorElement = document.createElement('div');
                tutorElement.textContent = `${tutorNumber}. ${tutor.tutorFirstName} ${tutor.tutorLastName} - ${tutor.tutorEmail}  -  ${tutor.availability} & ${tutor.deliveryMode}`;
                tutorList.appendChild(tutorElement);
                tutorNumber++;
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
});
// Add event listeners to the navigation menu items (li elements)
const homeLink = document.getElementById('home');
homeLink.addEventListener('click', function () {


    // Redirect to the "home.html" page
    window.location.href = '/Client/views/home.html';
});