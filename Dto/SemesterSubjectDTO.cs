namespace ESA.Dto
{
    public class SemesterSubjectDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Regulation { get; set; }
        public bool? Elective { get; set; }
        public int[]? SemesterIds { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Code)}={Code}, {nameof(Name)}={Name}, {nameof(Regulation)}={Regulation}, {nameof(Elective)}={Elective.ToString()}, {nameof(SemesterIds)}={SemesterIds}}}";
        }
    }
}
