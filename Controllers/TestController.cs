using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESA.Data;
using ESA.Models;
using ESA.Dto;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ESA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/<TestController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var listOfStudentLists = new List<SeatingPlanResponeDTO>();

            using (var context = new ESAContext())
            {
                var examDate = DateOnly.Parse("26-07-2022");
                string session = "AN";
                var studentsCountByExam = await context.StudentsCountByExams.Where(data => data.Date.Equals(examDate) && data.Session.Equals(session)).ToListAsync();

                for (int i = 0; i < studentsCountByExam.Count; i++)
                {
                    var studentsList = new List<StudentsByExam>();
                    var examDetails = studentsCountByExam[i];
                    List<StudentsByExam> studentsByExam = await context.StudentsByExams.Where(studentDetails => studentDetails.ExamId == examDetails.AggregatedExamId).ToListAsync();
                    Console.WriteLine("---------Inside Student Details Loop----------");
                    studentsByExam.ForEach(data =>
                    {
                        studentsList.Add(data);
                    });
                    listOfStudentLists.Add(new SeatingPlanResponeDTO(examDetails.NumberOfStudents, examDetails.AggregatedExamId, studentsList));
                }

                //studentsByExam.ForEach(data => Console.WriteLine(data));
            }



            return Ok(new { Message = "Success", Data = listOfStudentLists });
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
