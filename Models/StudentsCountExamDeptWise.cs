using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class StudentsCountExamDeptWise
    {
        public int? SubjectId { get; set; }
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public string? Abbreviation { get; set; }
        public long? Count { get; set; }
    }
}
