using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class ExamRoom
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int RoomId { get; set; }

        public virtual Exam Exam { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
