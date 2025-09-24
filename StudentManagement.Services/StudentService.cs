using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;
using Repository;

namespace StudentServices
{
    /// <summary>
    /// Implements the <see cref="IStudentService"/> interface.
    /// Handles business logic and interacts with the <see cref="IStudentRepository"/>.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="StudentService"/>.
        /// </summary>
        /// <param name="studentRepository">Repository instance for student operations.</param>
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <inheritdoc/>
        public async Task<List<Student>> GetAsync()
        {
            try
            {
                return await _studentRepository.GetAsync();
            }
            catch
            {
                // Exception will be logged in controller
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Student?> GetAsync(string id)
        {
            try
            {
                return await _studentRepository.GetAsync(id);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Student> CreateAsync(Student student)
        {
            try
            {
                return await _studentRepository.CreateAsync(student);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(string id, Student student)
        {
            try
            {
                await _studentRepository.UpdateAsync(id, student);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(string id)
        {
            try
            {
                await _studentRepository.RemoveAsync(id);
            }
            catch
            {
                throw;
            }
        }

        // Not implemented methods
        public Task DeleteStudentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task GetStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(string id, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
