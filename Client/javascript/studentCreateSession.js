document.getElementById('studySessionForm').addEventListener('submit', function (event) {
    event.preventDefault();

    // Extract studentId from URL
    const urlParams = new URLSearchParams(window.location.search);
    const studentId = urlParams.get('studentId');
    console.log(studentId);

    let studySessionId;

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
            studySessionId = sessionData.id;

            return fetch(`http://localhost:5052/api/studySessions/${studySessionId}/assignStudent/${studentId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                }
            });
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to assign student to study session');
            }

            // Redirect to Matching Tutor Page with studentSessionID
            window.location.href = '/Client/views/studentTutorMatch.html?studySessionId=' + studySessionId;
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
    const studentId = urlParams.get('studentId');
    console.log(studentId);
    // Redirect to the "studyAssign.html" page
    window.location.href = `/Client/views/studentAssignedTutors.html?studentId=${studentId}`
});