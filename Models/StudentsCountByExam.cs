using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class StudentsCountByExam
    {
        public int? AggregatedExamId { get; set; }
        public DateOnly? Date { get; set; }
        public string? Session { get; set; }
        public long? NumberOfStudents { get; set; }
    }
}
