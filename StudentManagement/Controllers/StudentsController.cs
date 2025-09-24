using Microsoft.AspNetCore.Mvc;
using StudentModels;
using StudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// Controller for managing student-related API endpoints.
    /// Handles CRUD operations for students.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="StudentsController"/>.
        /// </summary>
        /// <param name="studentService">Service to manage student operations.</param>
        /// <param name="logger">Logger instance for logging errors and information.</param>
        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Validates whether the provided string is a valid MongoDB ObjectId.
        /// </summary>
        /// <param name="id">ID string to validate.</param>
        /// <returns>True if valid, false otherwise.</returns>
        private bool IsValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }

        /// <summary>
        /// Retrieves all students.
        /// GET: api/students
        /// </summary>
        /// <returns>List of students.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            try
            {
                var students = await _studentService.GetAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all students");
                return StatusCode(500, "Could not fetch students");
            }
        }

        /// <summary>
        /// Retrieves a student by their ID.
        /// GET: api/students/{id}
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <returns>The student object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID format.");

            try
            {
                var student = await _studentService.GetAsync(id);
                if (student == null)
                    return NotFound($"Student with ID {id} not found");

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching student with ID {id}");
                return StatusCode(500, "Could not fetch student");
            }
        }

        /// <summary>
        /// Creates a new student.
        /// POST: api/students
        /// </summary>
        /// <param name="student">Student object to create.</param>
        /// <returns>The created student object.</returns>
        [HttpPost]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            // Validate model state
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Business validations
            if (student.Courses == null || student.Courses.Length == 0 || student.Courses.Any(c => string.IsNullOrWhiteSpace(c)))
                return BadRequest("Student must have at least one valid course.");
            if (student.Courses.Length > 5)
                return BadRequest("Cannot enroll in more than 5 courses.");
            if (student.Age < 10 || student.Age > 120)
                return BadRequest("Age must be between 10 and 120.");

            try
            {
                var created = await _studentService.CreateAsync(student);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student");
                return StatusCode(500, "Could not create student");
            }
        }

        /// <summary>
        /// Updates an existing student by ID.
        /// PUT: api/students/{id}
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <param name="student">Updated student object.</param>
        /// <returns>NoContent if successful, otherwise appropriate error response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Student student)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID format.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existing = await _studentService.GetAsync(id);
                if (existing == null)
                    return NotFound($"Student with ID {id} not found");

                // Business validations
                if (student.Courses == null || student.Courses.Length == 0)
                    return BadRequest("Student must have at least one course.");
                if (student.Courses.Length > 5)
                    return BadRequest("Cannot enroll in more than 5 courses.");
                if (student.Age < 10 || student.Age > 100)
                    return BadRequest("Age must be between 10 and 100.");

                await _studentService.UpdateAsync(id, student);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating student with ID {id}");
                return StatusCode(500, "Could not update student");
            }
        }

        /// <summary>
        /// Deletes a student by ID.
        /// DELETE: api/students/{id}
        /// </summary>
        /// <param name="id">Student ID to delete.</param>
        /// <returns>NoContent if deleted, otherwise appropriate error response.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID format.");

            try
            {
                var existing = await _studentService.GetAsync(id);
                if (existing == null)
                    return NotFound($"Student with ID {id} not found");

                await _studentService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting student with ID {id}");
                return StatusCode(500, "Could not delete student");
            }
        }
    }
}
