document.addEventListener('DOMContentLoaded', function () {
    const urlParams = new URLSearchParams(window.location.search);
    // Using tutorEmail instead of tutorId
    const tutorEmail = urlParams.get('tutorEmail');

    if (!tutorEmail) {
        console.error("Tutor email not provided in the URL.");
        alert("Tutor email not provided in the URL.");
        return;
    }

    // Using the endpoint to get tutor by email
    fetch(`/api/tutors/getByEmail/${tutorEmail}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Failed to fetch tutor data. Status: ${response.status}`);
            }
            return response.json();
        })
        .then(tutor => {
            document.getElementById("tutorName").innerText = `${tutor.firstName} ${tutor.lastName}`;

            const subjectsList = document.getElementById("subjectsList");
            subjectsList.innerHTML = "";
            tutor.studySessions.forEach(session => {
                const listItem = document.createElement("li");
                listItem.textContent = `${session.subject.name} - ${session.availability} - ${session.deliveryMode}`;
                subjectsList.appendChild(listItem);
            });

            document.getElementById("availability").innerText = tutor.availability;
            document.getElementById("deliveryMode").innerText = tutor.deliveryMode;

            document.getElementById("tutorDetails").style.display = "block";
        })
        .catch(error => {
            console.error("Error fetching tutor data:", error);
            alert("Failed to fetch tutor data. Please check the tutor email and try again.");
        });
});
