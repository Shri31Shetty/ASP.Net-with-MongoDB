using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Models;

namespace Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        public StudentRepository(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }

        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        public async Task<Student?> GetAsync(string id)
        {
            return await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _students.DeleteOneAsync(student => student.Id == id);
        }

        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
        }
    }
}
