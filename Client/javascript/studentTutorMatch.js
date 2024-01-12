
function enrollTutor(studySessionId, studentId) {

    console.log(`Enrolling the tutorSessionID ${studySessionId} with the student ${studentId}`);

    //Make a PUT request to assign the student to the tutor's match
    fetch(`http://localhost:5052/api/studysessions/tutorMatch/${studySessionId}/assignStudent/${studentId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to assign student to tutor match');
            }
            console.log('Student assigned to tutor match successfully');

            // Redirect the student to studentAssignedTutors.html with the studentId parameter
            const redirectUrl = `/Client/views/studentAssignedTutors.html?studentId=${studentId}`;
            window.location.href = redirectUrl;
        })
        .catch(error => {
            console.error('Error assigning student to tutor match:', error);
        });
}


document.addEventListener('DOMContentLoaded', function () {
    // Extract studySessionId from URL
    const urlParams = new URLSearchParams(window.location.search);
    const studySessionId = urlParams.get('studySessionId');
    console.log('studySessionId:', studySessionId);

    // Fetch matched study sessions data from the specific endpoint
    fetch(`http://localhost:5052/api/studysessions/tutorMatch/${studySessionId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch matched study sessions data');
            }
            return response.json();
        })
        .then(data => {
            console.log('Fetched data:', data);

            const matchedStudySessions = data.matchedStudySessions;
            const studentId = data.studentId;

            // Display matched tutor data in a table
            const tutorTableBody = document.getElementById('matchedTutorList');
            let tutorNumber = 1; // Initialize tutor number

            matchedStudySessions.forEach(studySession => {
                console.log(studySession.tutorSessionID);
                const tutorElement = document.createElement('div');
                tutorElement.textContent = `${tutorNumber}. ${studySession.tutorFirstName} ${studySession.tutorLastName} - ${studySession.tutorEmail}`;

                // Create "Enroll" button
                const enrollButton = document.createElement('button');
                enrollButton.textContent = 'Enroll';
                enrollButton.addEventListener('click', function () {
                    enrollTutor(studySession.tutorSessionID, studentId);
                });

                // Increment the tutor number for the next iteration
                tutorNumber++;

                // Append both the tutor information and the "Enroll" button to the container
                tutorElement.appendChild(enrollButton);
                tutorTableBody.appendChild(tutorElement);
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
});// Add event listeners to the navigation menu items (li elements)
const homeLink = document.getElementById('home');
homeLink.addEventListener('click', function () {


    // Redirect to the "home.html" page
    window.location.href = '/Client/views/home.html';
});
