using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Exam
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public DateOnly? Date { get; set; }
        public string? Session { get; set; }
        public string Type { get; set; } = null!;
        public string? Status { get; set; }
        public string? SemYear { get; set; }

        public virtual Subject? Subject { get; set; } = null!;
    }
}
