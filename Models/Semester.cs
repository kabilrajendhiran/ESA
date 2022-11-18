using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Semester
    {
        public Semester()
        {
            SemesterSubjects = new HashSet<SemesterSubject>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public decimal Nth { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<SemesterSubject> SemesterSubjects { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
