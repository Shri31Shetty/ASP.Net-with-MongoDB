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

namespace StudentManagement.Test
{
    public class StudentManagementAPITest
    {
        private StudentsController GetController(Mock<IStudentService> mockService)
        {
            var mockLogger = new Mock<ILogger<StudentsController>>();
            return new StudentsController(mockService.Object, mockLogger.Object);
        }

        private const string ValidObjectId = "507f1f77bcf86cd799439011"; // valid MongoDB ObjectId

        [Fact]
        public async Task Get_ReturnsListOfStudents()
        {
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync()).ReturnsAsync(new List<Student>
            {
                new Student { Id = ValidObjectId, Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } }
            });

            var controller = GetController(mockService);

            var actionResult = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var students = Assert.IsType<List<Student>>(okResult.Value);
            students.Should().HaveCount(1);
            students[0].Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task GetById_ReturnsStudent_WhenExists()
        {
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync(
                new Student { Id = ValidObjectId, Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } }
            );

            var controller = GetController(mockService);

            var actionResult = await controller.Get(ValidObjectId);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var student = Assert.IsType<Student>(okResult.Value);
            student.Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotExists()
        {
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            var actionResult = await controller.Get(ValidObjectId);

            Assert.IsType<NotFoundObjectResult>(actionResult.Result); // updated
        }

        [Fact]
        public async Task Create_ReturnsCreatedStudent()
        {
            var student = new Student { Id = ValidObjectId, Name = "Lakshmi", Age = 20, Gender = "Female", IsGraduated = false, Courses = new[] { "Math" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.CreateAsync(It.IsAny<Student>())).ReturnsAsync(student);

            var controller = GetController(mockService);

            var actionResult = await controller.Create(student);

            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnedStudent = Assert.IsType<Student>(createdResult.Value);
            returnedStudent.Name.Should().Be("Lakshmi");
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenStudentExists()
        {
            var student = new Student { Id = ValidObjectId, Name = "Updated", Age = 21, Gender = "Male", IsGraduated = false, Courses = new[] { "Math" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync(student);
            mockService.Setup(s => s.UpdateAsync(ValidObjectId, student)).Returns(Task.CompletedTask);

            var controller = GetController(mockService);

            var result = await controller.Update(ValidObjectId, student);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            var student = new Student { Id = ValidObjectId, Name = "Updated" };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            var result = await controller.Update(ValidObjectId, student);

            Assert.IsType<NotFoundObjectResult>(result); // updated
        }

        [Fact]
        public async Task Remove_ReturnsNoContent_WhenStudentExists()
        {
            var student = new Student { Id = ValidObjectId, Name = "To Delete", Age = 22, Gender = "Male", IsGraduated = false, Courses = new[] { "Science" } };
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync(student);
            mockService.Setup(s => s.RemoveAsync(ValidObjectId)).Returns(Task.CompletedTask);

            var controller = GetController(mockService);

            var result = await controller.Remove(ValidObjectId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync(ValidObjectId)).ReturnsAsync((Student?)null);

            var controller = GetController(mockService);

            var result = await controller.Remove(ValidObjectId);

            Assert.IsType<NotFoundObjectResult>(result); // updated
        }
    }
}
