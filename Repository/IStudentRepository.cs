using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;

namespace Repository
{
    /// <summary>
    /// Defines the contract for Student repository operations.
    /// This interface provides asynchronous CRUD operations for the Student entity.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Retrieves all students from the database asynchronously.
        /// </summary>
        /// <returns>A list of all <see cref="Student"/> objects.</returns>
        Task<List<Student>> GetAsync();

        /// <summary>
        /// Retrieves a single student by its unique ID asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student.</param>
        /// <returns>The <see cref="Student"/> object if found; otherwise, null.</returns>
        Task<Student?> GetAsync(string id);

        /// <summary>
        /// Creates a new student in the database asynchronously.
        /// </summary>
        /// <param name="student">The <see cref="Student"/> object to be created.</param>
        /// <returns>The created <see cref="Student"/> object.</returns>
        Task<Student> CreateAsync(Student student);

        /// <summary>
        /// Updates an existing student in the database asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student to update.</param>
        /// <param name="student">The updated <see cref="Student"/> object.</param>
        Task UpdateAsync(string id, Student student);

        /// <summary>
        /// Deletes a student from the database asynchronously by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the student to delete.</param>
        Task RemoveAsync(string id);
    }
}
