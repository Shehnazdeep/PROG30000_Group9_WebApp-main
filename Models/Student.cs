using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models;

namespace PROG30000_Group9_WebApp.Models
{
    public class Student : Person
    {
        public Guid Id { get; set; }
        public string School { get; set; }
        public List<StudySession> StudySessions { get; set; }
    }
}