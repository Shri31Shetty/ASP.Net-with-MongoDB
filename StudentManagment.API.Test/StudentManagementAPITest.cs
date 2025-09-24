using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;
using StudentServices;
using StudentManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace StudentManagment.API.Test
{
    public class StudentManagementAPITest
    {
        private StudentsController GetController(Mock<IStudentService> mockService)
        {
            var mockLogger = new Mock<ILogger<StudentsController>>();
            return new StudentsController(mockService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsListOfStudents()
        {
            // Arrange
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync()).ReturnsAsync(new List<Student>
            {
                new Student { Id = "1", Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } }
            });

            var controller = GetController(mockService);

            // Act
            var actionResult = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var students = Assert.IsType<List<Student>>(okResult.Value);
            students.Should().HaveCount(1);
            students[0].Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task GetById_ReturnsStudent_WhenExists()
        {
            // Arrange
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("1")).ReturnsAsync(
                new Student { Id = "1", Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } }
            );

            var controller = GetController(mockService);

            // Act
            var actionResult = await controller.Get("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var student = Assert.IsType<Student>(okResult.Value);
            student.Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("2")).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            // Act
            var actionResult = await controller.Get("2");

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedStudent()
        {
            // Arrange
            var student = new Student { Id = "1", Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.CreateAsync(It.IsAny<Student>())).ReturnsAsync(student);

            var controller = GetController(mockService);

            // Act
            var actionResult = await controller.Create(student);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnedStudent = Assert.IsType<Student>(createdResult.Value);
            returnedStudent.Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenStudentExists()
        {
            // Arrange
            var student = new Student { Id = "1", Name = "Updated", Age = 21, Gender = "Male", IsGraduated = false, Courses = new[] { "Math" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("1")).ReturnsAsync(student);
            mockService.Setup(s => s.UpdateAsync("1", student)).Returns(Task.CompletedTask);

            var controller = GetController(mockService);

            // Act
            var result = await controller.Update("1", student);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            var student = new Student { Id = "1", Name = "Updated" };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("1")).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            // Act
            var result = await controller.Update("1", student);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsNoContent_WhenStudentExists()
        {
            // Arrange
            var student = new Student { Id = "1", Name = "To Delete", Age = 22, Gender = "Male", IsGraduated = false, Courses = new[] { "Science" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("1")).ReturnsAsync(student);
            mockService.Setup(s => s.RemoveAsync("1")).Returns(Task.CompletedTask);

            var controller = GetController(mockService);

            // Act
            var result = await controller.Remove("1");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync("1")).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            // Act
            var result = await controller.Remove("1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
