using System.Collections.Generic;
using Models;

namespace Repository
{
    public interface IStudentRepository
    {
        List<Student> Get();
        Student Get(string id);
        Student Create(Student student);
        void Update(string id, Student student);
        void Remove(string id);
    }
}
