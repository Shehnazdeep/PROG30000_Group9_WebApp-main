using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROG30000_Group9_WebApp.Models;
using PROG30000_Group9_WebApp.Services;
using Microsoft.EntityFrameworkCore;

//Tharushi's Controller

namespace PROG30000_Group9_WebApp.Controllers
{
    [ApiController]
    // /api/studySessions
    [Route("api/[controller]")]
    public class StudySessionsController : ControllerBase
    {
        private TutorConnectDB _db = new TutorConnectDB();

        //POST /api/studysessions
        [HttpPost]
        public async Task<IActionResult> CreateStudySession(StudySession studySession)
        {
            await _db.StudySessions.AddAsync(studySession);
            await _db.SaveChangesAsync();
            return Created("", studySession);
        }

        //GET /api/studysessions
        [HttpGet]
        public async Task<IActionResult> GetAllStudySession()
        {
            var result = await _db.StudySessions.ToListAsync();
            if (result == null)
                return NotFound("No study sessions found");
            return Ok(result);
        }

        //GET /api/studysessions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudySessionById(Guid id)
        {
            var studySession = await _db.StudySessions.FirstOrDefaultAsync(x => x.Id == id);
            if (studySession == null)
                return NotFound("Study Session Not Found");
            return Ok(studySession);
        }

        [HttpGet("tutorMatch/{studySessionId}/GetStudentId")]
        public async Task<IActionResult> GetStudentInfoFromStudentSessionId(Guid studySessionId)
        {
            var studySession = await _db.StudySessions
                .Where(x => x.Id == studySessionId)
                .Include(x => x.Student) // Ensure Student property is included
                .FirstOrDefaultAsync();

            if (studySession != null)
            {
                var studentInfo = new
                {
                    StudentId = studySession.Student?.Id,
                    FirstName = studySession.Student?.FirstName,
                    LastName = studySession.Student?.LastName,
                    Email = studySession.Student?.Email,

                };

                return Ok(studentInfo);
            }
            else
            {
                return NotFound($"Study session with ID {studySessionId} not found.");
            }
        }


        //GET /api/studysessions/tutorMatch/{id}
        [HttpGet("tutorMatch/{id}")]
        public async Task<IActionResult> GetTutorSessionsThatMatchesWithStudentSessions(Guid id)
        {
            var studentStudySession = await _db.StudySessions
                            .Where(x => x.Id == id)
                            .Include(x => x.Student) // Ensure Student property is included
                            .FirstOrDefaultAsync();

            if (studentStudySession == null)
            {
                // If the student's study session is not found, return NotFound
                return NotFound($"Study session with ID {id} not found.");
            }


            var studentId = studentStudySession.Student.Id;

            // Fetch all tutor study sessions that have a tutor assigned
            var allTutorSessions = await _db.StudySessions
                .Where(x => x.Tutor != null && x.Id != id).Include(x => x.Tutor)// Exclude the student's session
                .ToListAsync();

            var matchingTutorSessions = allTutorSessions
                .Where(a =>
                    a.Student == null &&
                    a.Availability == studentStudySession.Availability &&
                    a.DeliveryMode == studentStudySession.DeliveryMode)
                .Select(tutorSession => new
                {

                    TutorSessionID = tutorSession.Id,
                    TutorId = tutorSession.Tutor.Id,
                    TutorFirstName = tutorSession.Tutor.FirstName,
                    TutorLastName = tutorSession.Tutor.LastName,
                    TutorEmail = tutorSession.Tutor.Email,

                })
                .ToList();

            var result = new
            {
                MatchedStudySessions = matchingTutorSessions,
                StudentId = studentId
            };


            return Ok(result);

        }

        //DELETE /api/studysessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionById(Guid id)
        {
            var studySession = await _db.StudySessions.FirstOrDefaultAsync(x => x.Id == id);
            if (studySession == null)
                return NotFound("Study Session Not Found");
            _db.Remove(studySession);
            await _db.SaveChangesAsync();
            return Ok("Study session deleted succesfully!");
        }

        //PUT /api/studysessions/{studySessionId}/assignTutor/{tutorId}
        [HttpPut("{studySessionId}/assignTutor/{tutorId}")]
        public async Task<IActionResult> AssignTutor(Guid studySessionId, Guid tutorId)
        {
            var studySessionInDb = await _db.StudySessions.FindAsync(studySessionId);
            var tutorInDb = await _db.Tutors.FindAsync(tutorId);

            if (studySessionInDb == null || tutorInDb == null)
                return BadRequest(new
                {
                    Message = "One of the ids does not exist."
                });

            studySessionInDb.Tutor = tutorInDb;
            await _db.SaveChangesAsync();
            return Ok();
        }
        //PUT /api/studysessions/{studySessionId}/assignStudent/{studentId}
        [HttpPut("{studySessionId}/assignStudent/{studentId}")]
        public async Task<IActionResult> AssignStudentToStudentStudySession(Guid studySessionId, Guid studentId)
        {
            var studySessionInDb = await _db.StudySessions.FindAsync(studySessionId);
            var studentInDb = await _db.Students.FindAsync(studentId);

            if (studySessionInDb == null || studentInDb == null)
                return BadRequest(new
                {
                    Message = "One of the ids does not exist."
                });

            studySessionInDb.Student = studentInDb;
            await _db.SaveChangesAsync();
            return Ok();
        }
        //PUT /api/studysessions/{studySessionId}/assignStudent/{studentId}
        [HttpPut("tutorMatch/{studySessionId}/assignStudent/{studentId}")]
        public async Task<IActionResult> AssignStudentToTutorStudySession(Guid studySessionId, Guid studentId)
        {
            var studySessionInDb = await _db.StudySessions.FindAsync(studySessionId);
            var studentInDb = await _db.Students.FindAsync(studentId);

            if (studySessionInDb == null || studentInDb == null)
                return BadRequest(new
                {
                    Message = "One of the ids does not exist."
                });

            studySessionInDb.Student = studentInDb;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}