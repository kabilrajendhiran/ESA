using System;
using System.Collections.Generic;
using ESA.Dto;

namespace ESA.Models
{
    public partial class SubjectAndDepartment
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Regulation { get; set; }
        public bool? Elective { get; set; }
        public SemesterDepartmentDTO[]? Details { get; set; }
    }
}
