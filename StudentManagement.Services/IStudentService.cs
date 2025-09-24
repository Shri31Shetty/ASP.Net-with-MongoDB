using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;

namespace StudentServices
{
    /// <summary>
    /// Defines the contract for student service operations.
    /// Provides asynchronous CRUD operations for managing students.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Retrieves all students asynchronously.
        /// </summary>
        /// <returns>List of <see cref="Student"/> objects.</returns>
        Task<List<Student>> GetAsync();

        /// <summary>
        /// Retrieves a student by ID asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student.</param>
        /// <returns>The <see cref="Student"/> object if found; otherwise, null.</returns>
        Task<Student?> GetAsync(string id);

        /// <summary>
        /// Creates a new student asynchronously.
        /// </summary>
        /// <param name="student">The <see cref="Student"/> object to create.</param>
        /// <returns>The created <see cref="Student"/> object.</returns>
        Task<Student> CreateAsync(Student student);

        /// <summary>
        /// Updates an existing student asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student to update.</param>
        /// <param name="student">The updated <see cref="Student"/> object.</param>
        Task UpdateAsync(string id, Student student);

        /// <summary>
        /// Removes a student asynchronously by ID.
        /// </summary>
        /// <param name="id">The unique ID of the student to remove.</param>
        Task RemoveAsync(string id);

        // The following methods are currently not implemented
        Task DeleteStudentAsync(string id);
        Task GetStudentByIdAsync(string id);
        Task UpdateStudentAsync(string id, Student student);
    }
}
