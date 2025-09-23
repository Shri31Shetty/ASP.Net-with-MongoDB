using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentModels;
using StudentServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        // GET: api/students
        [HttpGet]
    
        public async Task<ActionResult<List<Student>>> Get()
        {
            _logger.LogInformation("Get All Students");
            _logger.LogWarning("Its Warning");
            _logger.LogTrace("this is me");
            _logger.LogError("Error");
            var students = await _studentService.GetAsync();
            return students;
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
                return NotFound();
            return student;
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student student)
        {
            var createdStudent = await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = createdStudent.Id }, createdStudent);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Student student)
        {
            var existingStudent = await _studentService.GetAsync(id);
            if (existingStudent == null)
                return NotFound();

            await _studentService.UpdateAsync(id, student);
            return NoContent();
        }

        // DELETE: api/students/{id}
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
