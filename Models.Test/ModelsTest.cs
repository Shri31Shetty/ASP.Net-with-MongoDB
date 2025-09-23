using StudentModels;
using Xunit;

namespace StudentModels.Test
{
    public class ModelsTest
    {
        [Fact]
        public void Student_Properties_SetAndGet()
        {
            var student = new Student
            {
                Id = "507f1f77bcf86cd799439011",
                Name = "Lakshmi",
                IsGraduated = true,
                Courses = new[] { "Math", "Science" },
                Gender = "Female",
                Age = 22
            };

            Assert.Equal("507f1f77bcf86cd799439011", student.Id);
            Assert.Equal("Lakshmi", student.Name);
            Assert.True(student.IsGraduated);
            Assert.Contains("Math", student.Courses);
            Assert.Equal("Female", student.Gender);
            Assert.Equal(22, student.Age);
        }
    }
}