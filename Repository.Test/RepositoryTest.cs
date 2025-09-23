using System.Collections.Generic;
using System.Threading.Tasks;
using StudentModels;
using Repository;
using Moq;
using MongoDB.Driver;
using Xunit;

namespace Repository.Test
{
    public class RepositoryTest
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnStudent()
        {
            var mockCollection = new Mock<IMongoCollection<Student>>();
            var mockSettings = new Mock<IStudentStoreDatabaseSettings>();
            var mockClient = new Mock<IMongoClient>();
            var mockDatabase = new Mock<IMongoDatabase>();

            mockSettings.SetupGet(s => s.DatabaseName).Returns("TestDB");
            mockSettings.SetupGet(s => s.StudentCoursesCollectionName).Returns("Students");
            mockClient.Setup(c => c.GetDatabase("TestDB", null)).Returns(mockDatabase.Object);
            mockDatabase.Setup(d => d.GetCollection<Student>("Students", null)).Returns(mockCollection.Object);

            // Change this line:
            // var repo = new StudentRepository(mockSettings.Object, mockClient.Object);
            // To use the correct type from the StudentRepository namespace, e.g. StudentRepository.StudentRepository

            var repo = new Repository.StudentRepository(mockSettings.Object, mockClient.Object);
            var student = new Student { Id = "1", Name = "Test" };

            var result = await repo.CreateAsync(student);

            Assert.Equal("Test", result.Name);
        }
    }
}