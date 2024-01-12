using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string SubjectCode { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}