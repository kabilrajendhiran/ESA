using System;
using System.Collections.Generic;
using ESA.Dto;

namespace ESA.Models
{
    public partial class RoomAndSeat
    {
        public int? RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public int? Row { get; set; }
        public int? Col { get; set; }
        public int? Capacity { get; set; }
        public int? MaxCapacity { get; set; }
        public int? BlockId { get; set; }
        public SeatDto[]? Seats { get; set; }
    }
}
