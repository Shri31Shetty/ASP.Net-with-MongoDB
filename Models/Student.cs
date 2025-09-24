using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentModels
{
    /// <summary>
    /// Represents a student entity in the database.
    /// Maps to a MongoDB collection and contains basic student information.
    /// </summary>
    [BsonIgnoreExtraElements] // Ignores any extra fields in the MongoDB document that are not defined in this class
    public class Student
    {
        /// <summary>
        /// Unique identifier for the student.
        /// Stored as an ObjectId in MongoDB.
        /// </summary>
        [BsonId] // Marks this property as the primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Stores the Id as a MongoDB ObjectId but represented as string in C#
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Full name of the student.
        /// Required and maximum length of 100 characters.
        /// </summary>
        [BsonElement("name")] // Maps to the "name" field in MongoDB
        [Required(ErrorMessage = "Name is required.")] // Validation: cannot be null or empty
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // Validation: max length 100
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Indicates whether the student has graduated.
        /// </summary>
        [BsonElement("graduated")] // Maps to the "graduated" field in MongoDB
        public bool IsGraduated { get; set; }

        /// <summary>
        /// List of courses the student is enrolled in.
        /// Must contain at least one course.
        /// </summary>
        [BsonElement("courses")] // Maps to the "courses" field in MongoDB
        [MinLength(1, ErrorMessage = "At least one course is required.")] // Validation: at least one course
        public string[]? Courses { get; set; }

        /// <summary>
        /// Gender of the student.
        /// Must be "Male", "Female", or "Other".
        /// </summary>
        [BsonElement("gender")] // Maps to the "gender" field in MongoDB
        [Required(ErrorMessage = "Gender is required.")] // Validation: cannot be null or empty
        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be Male, Female, or Other.")] // Validation: allowed values
        public string Gender { get; set; } = String.Empty;

        /// <summary>
        /// Age of the student.
        /// Must be between 1 and 120.
        /// </summary>
        [BsonElement("age")] // Maps to the "age" field in MongoDB
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")] // Validation: valid range
        public int Age { get; set; }
    }
}
