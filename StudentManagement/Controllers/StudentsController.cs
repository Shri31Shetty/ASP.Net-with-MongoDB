using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            var students = await _studentService.GetAsync();
            return students;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
                return NotFound();
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student student)
        {
            var createdStudent = await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Student student)
        {
            var existingStudent = await _studentService.GetAsync(id);
            if (existingStudent == null)
                return NotFound();  

            await _studentService.UpdateAsync(id, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var existingStudent = await _studentService.GetAsync(id);
            if (existingStudent == null)
                return NotFound();

            await _studentService.RemoveAsync(id);
            return NoContent();
        }
    }
}
