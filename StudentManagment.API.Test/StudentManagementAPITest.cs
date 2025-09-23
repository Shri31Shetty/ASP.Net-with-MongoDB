using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;
using StudentServices;
using StudentManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace StudentManagment.API.Test
{
    public class StudentManagementAPITest
    {
        [Fact]
        public async Task Get_ReturnsListOfStudents()
        {
            // Arrange
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync()).ReturnsAsync(new List<Student>
            {
                new Student { Id = "1", Name = "Lakshmi" }
            });

            var mockLogger = new Mock<ILogger<StudentsController>>();
            var controller = new StudentsController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var students = Assert.IsType<List<Student>>(result.Value);
            Assert.Single(students);
            Assert.Equal("Lakshmi", students[0].Name);
        }
    }
}