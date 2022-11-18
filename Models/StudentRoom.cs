using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class StudentRoom
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
