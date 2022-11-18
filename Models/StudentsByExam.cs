using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class StudentsByExam
    {
        public int? ExamId { get; set; }
        public DateOnly? Date { get; set; }
        public string? Session { get; set; }
        public string? RegNo { get; set; }
        public int? SemesterId { get; set; }
        public int? StudentId { get; set; }
    }
}
