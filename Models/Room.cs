using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Room
    {
        public Room()
        {
            RoomSeats = new HashSet<RoomSeat>();
            StudentRooms = new HashSet<StudentRoom>();
        }

        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public int BlockId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int MaxCapacity { get; set; }

        public virtual Block Block { get; set; } = null!;
        public virtual ICollection<RoomSeat> RoomSeats { get; set; }
        public virtual ICollection<StudentRoom> StudentRooms { get; set; }
    }
}
