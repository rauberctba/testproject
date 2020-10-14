using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Interfaces;
using TestProject.Application.Services;
using TestProject.Domain.Entities;

namespace TestProject.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<Account>
    {
        public User User { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
        {
            private readonly IDatabaseContext context;
            private readonly IAccountCreditScoreCalculator creditScoreCalculator;

            public CreateAccountCommandHandler(IDatabaseContext context, IAccountCreditScoreCalculator creditScoreCalculator)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
                this.creditScoreCalculator = creditScoreCalculator ?? throw new ArgumentNullException(nameof(creditScoreCalculator));
            }

            public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var entity = new Account
                {
                    UserId = request.User.Id,
                    CreditScore = creditScoreCalculator.GetUserCreditScore(request.User)
                };

                context.Accounts.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
