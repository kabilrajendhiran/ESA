using ESA.Models;

namespace ESA.Dto
{
    public class SeatingPlanResponeDTO
    {
        public SeatingPlanResponeDTO(long? count, int? examId, List<StudentsByExam> studentsByExam)
        {
            Count = count;
            ExamId = examId;
            StudentsByExam = studentsByExam ?? throw new ArgumentNullException(nameof(studentsByExam));
        }

        public long? Count { get; set; }
        public int? ExamId { get; set; }
        public List<StudentsByExam> StudentsByExam { get; set; }
    }
}
