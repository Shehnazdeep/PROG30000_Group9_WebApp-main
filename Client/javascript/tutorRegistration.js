document.getElementById('tutorRegistrationForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const tutorData = {
        FirstName: document.getElementById('tutorFirstName').value,
        LastName: document.getElementById('tutorLastName').value,
        Email: document.getElementById('tutorEmail').value,
        EducationalBackground: document.getElementById('tutorEducationalBackground').value
    };

    fetch("http://localhost:5052/api/tutors/", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(tutorData)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            const tutorId = data.id; // Extract tutor ID
            window.location.href = '/Client/views/tutorCreateSession.html?tutorId=' + tutorId; // Redirect with tutorId
        })
        .catch((error) => {
            console.error('Error:', error);
            alert('Error registering tutor.');
        });
});

document.getElementById('signInForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var email = document.getElementById('signInEmail').value;
    fetch(`http://localhost:5052/api/tutors/getByEmail/${email}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);


            if (data !== null && data !== undefined) {
                window.location.href = '/Client/views/tutorCreateSession.html?tutorId=' + data;
            } else {
                console.error('Tutor ID not found or is null/undefined:', data);
                alert('Tutor ID not found. Please check your email.');
            }

        })
        .catch(error => {
            console.error('Error during sign-in:', error);
            alert('An error occurred during sign-in. Please try again.');
        });
});
