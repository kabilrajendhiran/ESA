using System;
using System.Collections.Generic;
using ESA.Dto;
namespace ESA.Models
{
    public partial class RoomWiseStudent
    {
        public int? ExamId { get; set; }
        public DateOnly? Date { get; set; }
        public string? Session { get; set; }
        public StudentRoomWiseDTO[]? Students { get; set; }
    }
}
