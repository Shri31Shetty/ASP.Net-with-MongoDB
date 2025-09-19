using System;
using Models;
using Services;
using Models;
using Services;
using Xunit;

namespace StudentManagement.Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public void PrintStudent_ShouldNotThrowException()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Lakshmi" };
            var service = new StudentService();

            // Act
            Exception exception = Record.Exception(() => service.PrintStudent(student));

            // Assert
            Assert.Null(exception);
        }
    }
}
