using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using StudentManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using StudentManagement.Controllers;

namespace StudentManagment.API.Test
{
    public class StudentManagementAPITest
    {
        [Fact]
        public async Task Get_ReturnsListOfStudents()
        {
            var mockService = new Mock<IStudentService>();
            mockService.Setup(s => s.GetAsync()).ReturnsAsync(new List<Student>
            {
                new Student { Id = "1", Name = "Lakshmi" }
            });

            var controller = new StudentsController(mockService.Object);
            var result = await controller.Get();

            var students = Assert.IsType<List<Student>>(result.Value);
            Assert.Single(students);
        }
    }
}