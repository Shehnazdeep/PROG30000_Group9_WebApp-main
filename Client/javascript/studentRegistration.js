document.getElementById('studentRegistrationForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const studentData = {
        FirstName: document.getElementById('studentFirstName').value,
        LastName: document.getElementById('studentLastName').value,
        Email: document.getElementById('studentEmail').value,
        School: document.getElementById('studentSchool').value
    };

    fetch("http://localhost:5052/api/students/", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(studentData)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            const studentId = data.id; // Extract student ID
            window.location.href = '/Client/views/studentCreateSession.html?studentId=' + studentId; // Redirect with studentID
        })
        .catch((error) => {
            console.error('Error:', error);
            alert('Error registering student.');
        });
});

document.getElementById('signInForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var email = document.getElementById('signInEmail').value;
    fetch(`http://localhost:5052/api/students/getByEmail/${email}`)
        .then(response => response.json())
        .then(data => {

            if (data !== null && data !== undefined) {
                window.location.href = '/Client/views/studentCreateSession.html?studentId=' + data;
            } else {
                console.error('Student ID not found or is null/undefined:', data);
                alert('Student ID not found. Please check your email.');
            }

        })
        .catch(error => {
            console.error('Error during sign-in:', error);
            alert('An error occurred during sign-in. Please try again.');
        });
});
