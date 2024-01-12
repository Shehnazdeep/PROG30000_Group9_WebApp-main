document.addEventListener("DOMContentLoaded", function () {
    const studentId = getStudentIdFromURL();

    if (!studentId) {
        signInStudent();
    } else {
        fetchStudentDetails(studentId);
    }
});

function signInStudent() {

    const signInEmail = prompt('Please enter your email:');

    if (signInEmail) {
        fetch(`/api/students/getByEmail/${signInEmail}`)
            .then(response => response.json())
            .then(data => {
                if (data && data.id) {
                    window.location.href = `/student.html?studentId=${data.id}`;
                } else {
                    alert('Student not found. Please check your email.');
                }
            })
            .catch(error => {
                console.error('Error during sign-in:', error);
                alert('An error occurred during sign-in. Please try again.');
            });
    }
}

async function fetchStudentDetails(studentId) {
    const apiUrl = `/api/students/${studentId}`;
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const studentData = await response.json();

        document.getElementById('studentName').innerText = `${studentData.firstName} ${studentData.lastName}`;

        fetchStudySessions(studentId);
    } catch (error) {
        console.error('Error fetching student details:', error);
    }
}

async function fetchStudySessions(studentId) {
    const apiUrl = `/api/students/${studentId}/studySessions`;
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const studySessions = await response.json();

        displayStudySessionDetails(studySessions);
    } catch (error) {
        console.error('Error fetching study sessions:', error);
    }
}

function displayStudySessionDetails(studySessions) {
    const studySessionDetailsDiv = document.getElementById('studySessionDetails');

    studySessionDetailsDiv.innerHTML = '';

    studySessions.forEach(session => {
        const sessionDiv = document.createElement('div');
        sessionDiv.innerHTML = `<p>Study Session ID: ${session.id}</p>
                                <p>Availability: ${session.availability}</p>
                                <p>Delivery Mode: ${session.deliveryMode}</p>`;
        studySessionDetailsDiv.appendChild(sessionDiv);
    });
}

function getStudentIdFromURL() {
    const urlParams = new URLSearchParams(window.location.search);
    const studentId = urlParams.get('studentId');
    return studentId;
}
