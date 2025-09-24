using Xunit;
using Moq;
using FluentAssertions;
using StudentModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentServices;
using Repository;

namespace StudentManagement.Tests
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _repoMock;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _repoMock = new Mock<IStudentRepository>();
            _service = new StudentService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedStudent()
        {
            var student = new Student { Name = "John Doe", Age = 25, Gender = "Male", IsGraduated = false };
            _repoMock.Setup(r => r.CreateAsync(It.IsAny<Student>())).ReturnsAsync(student);

            var result = await _service.CreateAsync(student);

            result.Should().NotBeNull();
            result.Name.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetAsyncById_ShouldReturnStudent_WhenExists()
        {
            var student = new Student { Id = "1", Name = "Jane" };
            _repoMock.Setup(r => r.GetAsync("1")).ReturnsAsync(student);

            var result = await _service.GetAsync("1");

            result.Should().NotBeNull();
            result!.Name.Should().Be("Jane");
        }

        [Fact]
        public async Task GetAsyncById_ShouldReturnNull_WhenNotExists()
        {
            _repoMock.Setup(r => r.GetAsync("2")).ReturnsAsync((Student?)null);

            var result = await _service.GetAsync("2");

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            var student = new Student { Id = "1", Name = "Updated", Age = 20, Gender = "Male" };
            _repoMock.Setup(r => r.UpdateAsync("1", student)).Returns(Task.CompletedTask).Verifiable();

            await _service.UpdateAsync("1", student);

            _repoMock.Verify(r => r.UpdateAsync("1", student), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_ShouldCallRepository()
        {
            _repoMock.Setup(r => r.RemoveAsync("1")).Returns(Task.CompletedTask).Verifiable();

            await _service.RemoveAsync("1");

            _repoMock.Verify(r => r.RemoveAsync("1"), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnList()
        {
            var students = new List<Student> { new Student { Id = "1", Name = "Test" } };
            _repoMock.Setup(r => r.GetAsync()).ReturnsAsync(students);

            var result = await _service.GetAsync();

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Test");
        }
    }
}
