using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Seat
    {
        public Seat()
        {
            RoomSeats = new HashSet<RoomSeat>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;

        public virtual ICollection<RoomSeat> RoomSeats { get; set; }
    }
}
