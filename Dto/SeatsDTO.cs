namespace ESA.Dto
{
    public class SeatsDTO
    {
        public int Id { get; set; }
        public bool Optional { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Optional)}={Optional.ToString()}, {nameof(Number)}={Number}}}";
        }
    }
}
