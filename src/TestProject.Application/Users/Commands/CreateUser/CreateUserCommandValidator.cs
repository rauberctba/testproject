using FluentValidation;

namespace TestProject.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Email)
                .EmailAddress()
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.MonthlyExpenses)
                .GreaterThan(0);

            RuleFor(v => v.MonthlySalary)
                .GreaterThan(0);
        }
    }
}
