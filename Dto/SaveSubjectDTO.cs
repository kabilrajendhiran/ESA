namespace ESA.Dto
{
    public class SaveSubjectDTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Regulation { get; set; }
        public bool? Elective { get; set; }

        public SemesterSubjectDTO[]? SemesterSubjects { get; set; }
    }
}
