using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class SemesterSubject
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }

        public virtual Semester Semester { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
