namespace ESA.Dto
{
    public class SaveRoomDto
    {
        public string RoomNumber { get; set; }
        public int BlockId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public SeatsDTO[] Seats { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(RoomNumber)}={RoomNumber}, {nameof(BlockId)}={BlockId.ToString()}, {nameof(Row)}={Row.ToString()}, {nameof(Col)}={Col.ToString()}, {nameof(Seats)}={Seats}}}";
        }
    }
}
