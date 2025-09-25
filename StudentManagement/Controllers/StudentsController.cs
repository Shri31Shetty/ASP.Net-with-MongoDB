using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using StudentModels;
using StudentServices;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// Controller for managing student CRUD operations.
    /// Supports JWT authentication and role-based authorization.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require JWT for all endpoints
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        /// <summary>
        /// Constructor injecting service and logger.
        /// </summary>
        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Checks if the given string is a valid MongoDB ObjectId.
        /// </summary>
        private bool IsValidObjectId(string id) => ObjectId.TryParse(id, out _);

        // ========================== GET ALL ==========================
        /// <summary>
        /// Retrieves all students.
        /// GET: api/students
        /// </summary>
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

        // ========================== GET BY ID ==========================
        /// <summary>
        /// Retrieves a student by ID.
        /// GET: api/students/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID");

            try
            {
                var student = await _studentService.GetAsync(id);
                if (student == null)
                {
                    _logger.LogWarning($"Student with ID {id} not found");
                    return new NotFoundObjectResult($"Student with ID {id} not found");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching student with ID {id}");
                return StatusCode(500, "Could not fetch student");
            }
        }

        // ========================== CREATE ==========================
        /// <summary>
        /// Creates a new student.
        /// POST: api/students
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")] // Only Admin/Moderator can create
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _studentService.CreateAsync(student);
                _logger.LogInformation($"Student created with ID {created.Id}");
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student");
                return StatusCode(500, "Could not create student");
            }
        }

        // ========================== UPDATE ==========================
        /// <summary>
        /// Updates an existing student by ID.
        /// PUT: api/students/{id}
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")] // Only Admin/Moderator can update
        public async Task<IActionResult> Update(string id, [FromBody] Student student)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID");

            try
            {
                var existing = await _studentService.GetAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning($"Update failed. Student with ID {id} not found.");
                    return new NotFoundObjectResult($"Student with ID {id} not found");
                }

                await _studentService.UpdateAsync(id, student);
                _logger.LogInformation($"Student with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating student with ID {id}");
                return StatusCode(500, "Could not update student");
            }
        }

        // ========================== DELETE ==========================
        /// <summary>
        /// Deletes a student by ID.
        /// DELETE: api/students/{id}
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public async Task<IActionResult> Remove(string id)
        {
            if (!IsValidObjectId(id))
                return BadRequest("Invalid student ID");

            try
            {
                var existing = await _studentService.GetAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning($"Delete failed. Student with ID {id} not found.");
                    return new NotFoundObjectResult($"Student with ID {id} not found");
                }

                await _studentService.RemoveAsync(id);
                _logger.LogInformation($"Student with ID {id} deleted successfully.");
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
