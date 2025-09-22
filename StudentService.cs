using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repository;

namespace Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Student>> GetAsync()
        {
            return _repository.GetAsync();
        }

        // You can add other methods as needed to match the repository interface
    }
}
