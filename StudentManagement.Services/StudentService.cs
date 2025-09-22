using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {       
            _studentRepository = studentRepository;
        }

        public Student Create(Student student)
        {
            return _studentRepository.Create(student);
        }

        public List<Student> Get()
        {
            return _studentRepository.Get();
        }

        public Student Get(string id)
        {
            return _studentRepository.Get(id);
        }

        public void Remove(string id)
        {
            _studentRepository.Remove(id);
        }

        public void Update(string id, Student student)
        {
            _studentRepository.Update(id, student);
        }
    }
}
