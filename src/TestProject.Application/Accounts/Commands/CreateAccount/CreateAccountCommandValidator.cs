using FluentValidation;
using System;
using TestProject.Application.Services;
using TestProject.Domain.Entities;

namespace TestProject.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IAccountCreditCheck creditCheck;

        public CreateAccountCommandValidator(IAccountCreditCheck creditCheck)
        {
            this.creditCheck = creditCheck ?? throw new ArgumentNullException(nameof(creditCheck));

            RuleFor(v => v.User)
                .NotNull()
                .Must(PassCreditCheck).WithMessage("Insufficient monthly income.");
        }

        public bool PassCreditCheck(User user)
        {
            return creditCheck.CheckUserCredit(user);
        }
    }
}
