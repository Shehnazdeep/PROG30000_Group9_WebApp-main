document.getElementById('studySessionForm').addEventListener('submit', function (event) {
    event.preventDefault();

    // Extract tutorId from URL
    const urlParams = new URLSearchParams(window.location.search);
    const tutorId = urlParams.get('tutorId');
    console.log(tutorId);
    // Collect the form data
    const availability = document.getElementById('availability').value;
    const deliveryMode = document.getElementById('deliveryMode').value;
    const subjectCode = document.getElementById('subjectCode').value;
    const category = document.getElementById('category').value;
    const description = document.getElementById('description').value;

    // Construct the StudySession object
    const studySession = {
        Availability: availability,
        DeliveryMode: deliveryMode,
        Subject: {
            SubjectCode: subjectCode,
            Category: category,
            Description: description
        }
    };

    // Make a POST request to create the StudySession
    fetch('http://localhost:5052/api/studySessions/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(studySession)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to create study session');
            }
            return response.json();
        })
        .then(sessionData => {
            console.log('Study Session Created:', sessionData);
            const studySessionId = sessionData.id;

            // Assign the tutor to the study session
            return fetch(`http://localhost:5052/api/studySessions/${studySessionId}/assignTutor/${tutorId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                }
            });
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to assign tutor to study session');
            }

            // Redirect to tutorAssignedStudents.html with tutorId
            window.location.href = '/Client/views/tutorAssignedStudents.html?tutorId=' + tutorId;
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error.message);
        });
});

// Add event listeners to the navigation menu items (li elements)
const homeLink = document.getElementById('home');
homeLink.addEventListener('click', function () {


    // Redirect to the "home.html" page
    window.location.href = '/Client/views/home.html';
});

const allStudySessionsLink = document.getElementById('allStudySessions');
allStudySessionsLink.addEventListener('click', function () {

    // Extract tutorId from URL
    const urlParams = new URLSearchParams(window.location.search);
    const tutorId = urlParams.get('tutorId');
    console.log(tutorId);
    // Redirect to the "studyAssign.html" page 
    window.location.href = '/Client/views/tutorAssignedStudents.html?tutorId=' + tutorId;
});