using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Department
    {
        public Department()
        {
            Semesters = new HashSet<Semester>();
        }

        public int Id { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public int? Code { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
