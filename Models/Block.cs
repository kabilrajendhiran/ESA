using System;
using System.Collections.Generic;

namespace ESA.Models
{
    public partial class Block
    {
        public Block()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
