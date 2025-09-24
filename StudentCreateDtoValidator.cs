using FluentValidation;
using StudentModels;

namespace StudentManagement.Validators  // <- add a namespace
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(s => s.Name).NotEmpty().MaximumLength(100);
            RuleFor(s => s.Age).InclusiveBetween(1, 120);
            RuleFor(s => s.Gender).NotEmpty().Must(g => new[] { "Male", "Female", "Other" }.Contains(g));
            RuleFor(s => s.Courses).NotEmpty();
            When(s => s.Address != null, () =>
            {
                RuleFor(s => s.Address.Street).NotEmpty();
                RuleFor(s => s.Address.City).NotEmpty();
                RuleFor(s => s.Address.State).NotEmpty();
                RuleFor(s => s.Address.Zip).Matches(@"^\d{5}(-\d{4})?$");
            });
        }
    }
}
