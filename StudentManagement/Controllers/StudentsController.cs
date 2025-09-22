using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

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
        public ActionResult<List<Student>> Get()
        {
            return _studentService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            var student = _studentService.Get(id);
            if (student == null)
                return NotFound();
            return student;
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            var createdStudent = _studentService.Create(student);
            return CreatedAtAction(nameof(Get), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Student student)
        {
            var existingStudent = _studentService.Get(id);
            if (existingStudent == null)
                return NotFound();

            _studentService.Update(id, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            var existingStudent = _studentService.Get(id);
            if (existingStudent == null)
                return NotFound();

            _studentService.Remove(id);
            return NoContent();
        }
    }
}
