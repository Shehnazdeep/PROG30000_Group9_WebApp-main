using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PROG30000_Group9_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace PROG30000_Group9_WebApp.Services
{
    public class TutorConnectDB : DbContext
    {
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TutorConnect.db");
        }
    }
}