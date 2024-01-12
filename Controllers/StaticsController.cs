using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG30000_Group9_WebApp.Services;

namespace PROG30000_Group9_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // /api/statics
    public class StaticsController : ControllerBase
    {
        private TutorConnectDB _db = new TutorConnectDB();

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var totalStudents = await _db.Students.CountAsync();
            var totalTutors = await _db.Tutors.CountAsync();
            var totalSubjects = await _db.Subjects.CountAsync();

            var statistics = new
            {
                TotalStudents = totalStudents,
                TotalTutors = totalTutors,
                TotalSubjects = totalSubjects
            };
            return Ok(statistics);
        }
    }
}