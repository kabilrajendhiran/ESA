using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class RoomSeat
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public int? SeatId { get; set; }
        public bool? Optional { get; set; }

        public virtual Room? Room { get; set; }
        public virtual Seat? Seat { get; set; }
    }
}
