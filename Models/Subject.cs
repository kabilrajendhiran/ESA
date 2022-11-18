using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Exams = new HashSet<Exam>();
            SemesterSubjects = new HashSet<SemesterSubject>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Regulation { get; set; }
        public bool? Elective { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<SemesterSubject> SemesterSubjects { get; set; }
    }
}
