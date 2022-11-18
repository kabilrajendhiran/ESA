using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentRooms = new HashSet<StudentRoom>();
        }

        public int Id { get; set; }
        public string RollNo { get; set; } = null!;
        public string RegNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; } = null!;
        public virtual ICollection<StudentRoom> StudentRooms { get; set; }
    }
}
