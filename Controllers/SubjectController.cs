using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESA.Data;
using ESA.Models;
using ESA.Dto;

namespace ESA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ESAContext _context;

        public SubjectController(ESAContext context)
        {
            _context = context;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        [HttpGet("Code/")]
        public async Task<IActionResult> GetSubjectCodes()
        {
            var results = await _context.Subjects.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToListAsync();
            return Ok(new { Message = "Success", Data = results });
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> GetSubjectDetails(int id)
        {
            var results = await _context.StudentsCountExamDeptWises.Where(x => x.SubjectId == id).Select(x => new
            {
                Dept = x.Abbreviation,
                Count = x.Count

            }).ToListAsync();
            return Ok(new { Message = "Success", Data = results });
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = _context.Subjects.Where(x => x.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(new { Message = "Success", Data = subject });
        }

        [HttpGet("AllDetails/")]
        public async Task<IActionResult> GetAllSubjectDetails()
        {
            //var subject = _context.Subjects.Include(x=>x.SemesterSubjects).ThenInclude(x=>x.Semester);
            var subject =await _context.SubjectAndDepartments.ToListAsync();

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(new { Message = "Success", Data = subject });
        }


        // PUT: api/Subject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSubject(SemesterSubjectDTO semesterSubjectDTO)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var subject = new Subject
                    {
                        Code = semesterSubjectDTO.Code,
                        Name = semesterSubjectDTO.Name,
                        Elective = semesterSubjectDTO.Elective,
                        Regulation = semesterSubjectDTO.Regulation
                    };

                    await _context.Subjects.AddAsync(subject);
                    await _context.SaveChangesAsync();

                    foreach (int semId in semesterSubjectDTO.SemesterIds)
                    {
                        var semesterSubject = new SemesterSubject { SubjectId = subject.Id, SemesterId = semId };
                        await _context.SemesterSubjects.AddAsync(semesterSubject);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return Problem(e.Message);
                }
                return Ok(new { Message = "Success", Data = semesterSubjectDTO });
            }

            //return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
