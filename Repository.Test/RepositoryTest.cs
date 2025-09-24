using Xunit;
using FluentAssertions;
using Mongo2Go;
using MongoDB.Driver;
using StudentModels;
using System;
using System.Threading.Tasks;
using Repository;

namespace StudentManagement.Tests
{
    public class StudentRepositoryTests : IDisposable
    {
        private readonly MongoDbRunner _mongoRunner;
        private readonly IMongoClient _client;
        private readonly StudentRepository _repository;

        public StudentRepositoryTests()
        {
            _mongoRunner = MongoDbRunner.Start();
            _client = new MongoClient(_mongoRunner.ConnectionString);

            var settings = new StudentStoreDatabaseSettings
            {
                DatabaseName = "TestDB",
                StudentCoursesCollectionName = "Students"
            };

            _repository = new StudentRepository(settings, _client);
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertStudent()
        {
            var student = new Student { Name = "Test Student", Gender = "Male", Age = 20, IsGraduated = false, Courses = new[] { "Math" } };
            var result = await _repository.CreateAsync(student);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();

            var fetched = await _repository.GetAsync(student.Id);
            fetched.Should().NotBeNull();
            fetched!.Name.Should().Be("Test Student");
        }

        [Fact]
        public async Task UpdateAsync_ShouldReplaceStudent()
        {
            var student = new Student { Name = "Before Update", Gender = "Male", Age = 22, IsGraduated = false, Courses = new[] { "Science" } };
            await _repository.CreateAsync(student);

            student.Name = "After Update";
            await _repository.UpdateAsync(student.Id, student);

            var fetched = await _repository.GetAsync(student.Id);
            fetched.Should().NotBeNull();
            fetched!.Name.Should().Be("After Update");
        }

        [Fact]
        public async Task RemoveAsync_ShouldDeleteStudent()
        {
            var student = new Student { Name = "To Delete", Gender = "Female", Age = 30, IsGraduated = true, Courses = new[] { "History" } };
            await _repository.CreateAsync(student);

            await _repository.RemoveAsync(student.Id);

            var fetched = await _repository.GetAsync(student.Id);
            fetched.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllStudents()
        {
            var student1 = new Student { Name = "S1", Gender = "Male", Age = 18, IsGraduated = false, Courses = new[] { "English" } };
            var student2 = new Student { Name = "S2", Gender = "Female", Age = 21, IsGraduated = true, Courses = new[] { "Physics" } };

            await _repository.CreateAsync(student1);
            await _repository.CreateAsync(student2);

            var all = await _repository.GetAsync();
            all.Should().NotBeNull();
            all.Should().HaveCount(2);
        }

        public void Dispose()
        {
            _mongoRunner.Dispose();
        }
    }
}
