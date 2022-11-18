using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESA.Data;
using ESA.Models;

namespace ESA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ESAContext _context;

        public ExamController(ESAContext context)
        {
            _context = context;
        }

        // GET: api/Exam
        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            var results = await _context.Exams.Include(e => e.Subject).Select(e => new
            {
                ExamId = e.Id,
                SubjectCode = e.Subject.Code,
                SubjectName = e.Subject.Name,
                Date = e.Date,
                Session = e.Session,
                SemYear = e.SemYear,
                Status = e.Status

            }).ToListAsync();

            return Ok(new { Message = "Success", Data = results });
        }

        // GET: api/Exam/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExam(int id)
        {
            var exam = _context.Exams.Include(e => e.Subject).Where(exam => exam.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(new { Message = "Success", data = exam });
        }

        // PUT: api/Exam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (id != exam.Id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Exam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }

        // DELETE: api/Exam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
