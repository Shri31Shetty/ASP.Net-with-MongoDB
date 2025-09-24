using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentModels
{
    /// <summary>
    /// Represents a student entity in the MongoDB database.
    /// Includes validation attributes and implements IValidatableObject
    /// for custom validations (like course content validation).
    /// </summary>
    [BsonIgnoreExtraElements] // Ignore extra fields in MongoDB document that are not in this class
    public class Student : IValidatableObject
    {
        /// <summary>
        /// Unique identifier for the student.
        /// Stored as a MongoDB ObjectId but represented as a string in C#.
        /// </summary>
        [BsonId] // Primary key in MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Store as ObjectId in DB but string in code
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the student.
        /// Required and cannot exceed 100 characters.
        /// </summary>
        [BsonElement("name")] // Maps to "name" field in MongoDB
        [Required(ErrorMessage = "Name is required.")] // Validation: required
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // Validation: max length
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the student has graduated.
        /// </summary>
        [BsonElement("graduated")] // Maps to "graduated" field
        public bool IsGraduated { get; set; }

        /// <summary>
        /// Array of courses the student is enrolled in.
        /// Must contain at least one course and cannot contain empty strings.
        /// </summary>
        [BsonElement("courses")] // Maps to "courses" field
        [MinLength(1, ErrorMessage = "At least one course is required.")] // Ensure at least one course
        public string[] Courses { get; set; } = Array.Empty<string>(); // Initialize to avoid null

        /// <summary>
        /// Gender of the student.
        /// Must be "Male", "Female", or "Other".
        /// </summary>
        [BsonElement("gender")] // Maps to "gender" field
        [Required(ErrorMessage = "Gender is required.")] // Cannot be null or empty
        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be Male, Female, or Other.")] // Allowed values
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Age of the student.
        /// Must be between 1 and 120.
        /// </summary>
        [BsonElement("age")] // Maps to "age" field
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")] // Range validation
        public int Age { get; set; }

        /// <summary>
        /// Custom validation logic.
        /// Checks that Courses array is not null, not empty, and does not contain blank strings.
        /// Called automatically by ASP.NET Core when model validation occurs.
        /// </summary>
        /// <param name="validationContext">Context for validation</param>
        /// <returns>A list of validation errors (if any)</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate that the Courses array is not null or empty
            if (Courses == null || Courses.Length == 0)
            {
                yield return new ValidationResult(
                    "At least one course is required.",
                    new[] { nameof(Courses) });
            }

            // Validate that no course is empty or whitespace
            if (Courses != null && Courses.Any(c => string.IsNullOrWhiteSpace(c)))
            {
                yield return new ValidationResult(
                    "Courses cannot contain empty or whitespace values.",
                    new[] { nameof(Courses) });
            }
        }
    }
}
