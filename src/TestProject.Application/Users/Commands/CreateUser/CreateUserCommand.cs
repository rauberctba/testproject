using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Interfaces;
using TestProject.Domain.Entities;

namespace TestProject.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }

        // Implementing handlers as a nested classes to improve discoverability. Consider
        // split into different files if command or handler are too big.
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
        {
            private readonly IDatabaseContext context;

            public CreateUserCommandHandler(IDatabaseContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    MonthlyExpenses = request.MonthlyExpenses,
                    MonthlySalary = request.MonthlySalary
                };

                context.Users.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }

}
