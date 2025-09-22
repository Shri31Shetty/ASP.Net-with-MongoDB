using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Repository;
using Moq;
using Xunit;

namespace Services.Test
{
    public class ServicesTest
    {
        [Fact]
        public async Task GetAsync_ReturnsStudents()
        {
            var mockRepo = new Mock<IStudentRepository>();
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(new List<Student>
            {
                new Student { Id = "1", Name = "Lakshmi" }
            });

            var service = new StudentService(mockRepo.Object);
            var students = await service.GetAsync();

            Assert.Single(students);
            Assert.Equal("Lakshmi", students[0].Name);
        }
    }
}