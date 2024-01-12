using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG30000_Group9_WebApp.Models;
using PROG30000_Group9_WebApp.Services;

namespace PROG30000_Group9_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private TutorConnectDB _db = new TutorConnectDB();

        //POST /api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            return Created("", student);
        }

        //GET /api/students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _db.Students.Include(x => x.StudySessions)
                                          .ToListAsync();
            if (result == null)
                return NotFound("Students not Found!");

            return Ok(result);
        }


        //GET /api/students/{studentId}
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(Guid studentId)
        {
            var student = await _db.Students
                                    .Include(x => x.StudySessions)
                                    .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        //GET api/students/getByEmail/{email}
        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetStudentIdByEmail(string email)
        {
            Console.WriteLine($"Searching for student with email: {email}");
            var student = await _db.Students.FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower());

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return NotFound("Student not found with the given email.");
            }

            Console.WriteLine($"Found student with ID: {student.Id}");

            return Ok(student.Id);
        }

        // /api/students/{studentId}/assignedTutors
        [HttpGet("{studentId}/assignedTutors")]
        public async Task<IActionResult> GetStudentWithTutors(Guid studentId)
        {
            var student = await _db.Students
                                    .Include(x => x.StudySessions)
                                        .ThenInclude(s => s.Tutor)
                                    .FirstOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
                return NotFound();

            // Extract the Tutors from the StudySessions, excluding null values
            var tutorsAssignedTothisStudentID = _db.StudySessions
                .Include(x => x.Student)
                .Where(s => s.Tutor != null && s.Student.Id == studentId)
                .Select(s => new
                {
                    TutorId = s.Tutor.Id,
                    TutorFirstName = s.Tutor.FirstName,
                    TutorLastName = s.Tutor.LastName,
                    TutorEmail = s.Tutor.Email,
                    Availability = s.Availability,
                    DeliveryMode = s.DeliveryMode

                })
                .Distinct()
                .ToList();

            var response = new
            {
                tutorsAssignedTothisStudentID = tutorsAssignedTothisStudentID,
                student = student
            };

            return Ok(response);
        }


        // DELETE /api/students/{studentId}
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudentById(Guid studentId)
        {
            var student = await _db.Students
                                    .Include(s => s.StudySessions)
                                        .ThenInclude(ss => ss.Subject)  // Include the Subject for eager loading
                                    .FirstOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
                return NotFound();

            // Remove associated study sessions and their subjects
            _db.StudySessions.RemoveRange(student.StudySessions);

            // Remove the student
            _db.Students.Remove(student);

            await _db.SaveChangesAsync();

            return Ok(new { Message = "Student, associated study sessions, and subjects deleted successfully." });
        }
    }
}