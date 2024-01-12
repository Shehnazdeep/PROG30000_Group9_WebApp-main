using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models;

//Zubear's Class

namespace PROG30000_Group9_WebApp.Models
{
    public class Tutor : Person
    {
        public Guid Id { get; set; }
        public string EducationalBackground { get; set; }
        public List<StudySession> StudySessions { get; set; }
    }
}