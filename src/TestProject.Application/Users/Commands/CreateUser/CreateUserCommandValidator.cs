using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Interfaces;

namespace TestProject.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IDatabaseContext context;

        public CreateUserCommandValidator(IDatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Email)
                .EmailAddress()
                .MaximumLength(100)
                .NotEmpty()
                .MustAsync(BeUniqueEmail).WithMessage("Email must be unique.");

            RuleFor(v => v.MonthlyExpenses)
                .GreaterThan(0);

            RuleFor(v => v.MonthlySalary)
                .GreaterThan(0);
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await context.Users
                .AllAsync(e => e.Email != email);
        }
    }
}
