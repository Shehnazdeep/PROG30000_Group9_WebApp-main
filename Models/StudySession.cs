using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Project.Models;

namespace PROG30000_Group9_WebApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeliveryMode
    {
        Online, InPerson, Hybrid
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Availability
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
    public class StudySession
    {
        public Guid Id { get; set; }

        public Availability Availability { get; set; }

        public Tutor? Tutor { get; set; }

        public Student? Student { get; set; }

        public DeliveryMode DeliveryMode { get; set; }

        public Subject Subject { get; set; }
    }
}