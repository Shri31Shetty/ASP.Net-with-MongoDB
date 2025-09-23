using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;
using Repository;

namespace StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAsync()
        {
            return await _studentRepository.GetAsync();
        }

        public async Task<Student?> GetAsync(string id)
        {
            return await _studentRepository.GetAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            return await _studentRepository.CreateAsync(student);
        }

        public async Task UpdateAsync(string id, Student student)
        {
            await _studentRepository.UpdateAsync(id, student);
        }

        public async Task RemoveAsync(string id)
        {
            await _studentRepository.RemoveAsync(id);
        }
    }
}
