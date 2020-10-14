using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Interfaces;
using TestProject.Domain.Entities;

namespace TestProject.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
        {
            private readonly IDatabaseContext context;

            public GetUserQueryHandler(IDatabaseContext context)
            {
                this.context = context;
            }

            public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                return context.Users.FirstOrDefaultAsync(e => e.Id == request.Id);
            }
        }
    }
}
