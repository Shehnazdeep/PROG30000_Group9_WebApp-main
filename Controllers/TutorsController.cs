using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROG30000_Group9_WebApp.Models;
using PROG30000_Group9_WebApp.Services;
using Microsoft.EntityFrameworkCore;

//Zubear's Controller

namespace PROG30000_Group9_WebApp.Controllers
{
    [ApiController]
    // /api/tutors
    [Route("api/[controller]")]
    public class TutorsController : ControllerBase
    {
        private TutorConnectDB _db = new TutorConnectDB();

        //POST /api/tutors
        [HttpPost]
        public async Task<IActionResult> CreateTutor(Tutor tutor)
        {
            await _db.Tutors.AddAsync(tutor);
            await _db.SaveChangesAsync();
            return Created("", tutor);
        }

        //GET /api/tutors
        [HttpGet]
        public async Task<IActionResult> GetAllTutors()
        {
            var result = await _db.Tutors.Include(x => x.StudySessions)
                                          .ToListAsync();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //GET /api/tutors/{tutorId}
        [HttpGet("{tutorId}")]
        public async Task<IActionResult> GetTutorById(Guid tutorId)
        {
            var tutor = await _db.Tutors
                                    .Include(x => x.StudySessions)
                                    .FirstOrDefaultAsync(x => x.Id == tutorId);
            if (tutor == null)
                return NotFound();

            return Ok(tutor);
        }

        //GET api/tutors/getByEmail/{email}
        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetTutorIdByEmail(string email)
        {
            Console.WriteLine($"Searching for tutor with email: {email}");
            var tutor = await _db.Tutors.FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower());

            if (tutor == null)
            {
                Console.WriteLine("Tutor not found.");
                return NotFound("Tutor not found with the given email.");
            }

            Console.WriteLine($"Found tutor with ID: {tutor.Id}");

            return Ok(tutor.Id);
        }

        // GET api/tutors/{tutorId}/assignedStudents
        [HttpGet("{tutorId}/assignedStudents")]
        public async Task<IActionResult> GetAssignedStudentsByTutorId(Guid tutorId)
        {
            var tutor = await _db.Tutors
                                  .Include(t => t.StudySessions)
                                  .ThenInclude(s => s.Student)
                                  .FirstOrDefaultAsync(t => t.Id == tutorId);

            if (tutor == null)
                return NotFound();

            // Extract the Students from the StudySessions, excluding null values
            var assignedStudents = tutor.StudySessions
                                        .Where(s => s.Student != null)
                                        .Select(s => s.Student)
                                        .Distinct()
                                        .ToList();

            return Ok(assignedStudents);
        }



        // DELETE /api/tutors/{tutorId}
        [HttpDelete("{tutorId}")]
        public async Task<IActionResult> DeleteTutorById(Guid tutorId)
        {
            var tutor = await _db.Tutors
                                    .Include(t => t.StudySessions)
                                        .ThenInclude(ss => ss.Subject) // Include the Subject for eager loading
                                    .FirstOrDefaultAsync(x => x.Id == tutorId);

            if (tutor == null)
                return NotFound();

            // Remove associated study sessions and their subjects
            _db.StudySessions.RemoveRange(tutor.StudySessions);

            // Remove the tutor
            _db.Tutors.Remove(tutor);

            await _db.SaveChangesAsync();

            return Ok(new { Message = "Tutor, associated study sessions, and subjects deleted successfully." });
        }

    }
}