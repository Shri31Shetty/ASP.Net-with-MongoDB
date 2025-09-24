using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using StudentModels;

namespace Repository
{
    /// <summary>
    /// Repository implementation for managing Student entities in MongoDB.
    /// Handles CRUD operations using the <see cref="IMongoCollection{Student}"/>.
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentRepository"/> class.
        /// </summary>
        /// <param name="settings">Database settings including collection name and database name.</param>
        /// <param name="mongoClient">The MongoDB client to access the database.</param>
        public StudentRepository(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
        }

        /// <summary>
        /// Inserts a new student into the database asynchronously.
        /// </summary>
        /// <param name="student">The <see cref="Student"/> object to insert.</param>
        /// <returns>The inserted <see cref="Student"/> object.</returns>
        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }

        /// <summary>
        /// Retrieves all students from the database asynchronously.
        /// </summary>
        /// <returns>A list of all <see cref="Student"/> objects.</returns>
        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves a single student by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student.</param>
        /// <returns>The <see cref="Student"/> object if found; otherwise, null.</returns>
        public async Task<Student?> GetAsync(string id)
        {
            return await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Deletes a student from the database by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student to delete.</param>
        public async Task RemoveAsync(string id)
        {
            await _students.DeleteOneAsync(student => student.Id == id);
        }

        /// <summary>
        /// Updates an existing student in the database asynchronously.
        /// </summary>
        /// <param name="id">The unique ID of the student to update.</param>
        /// <param name="student">The updated <see cref="Student"/> object.</param>
        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
        }
    }
}
