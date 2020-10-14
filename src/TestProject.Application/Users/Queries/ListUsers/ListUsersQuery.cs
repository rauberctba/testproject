using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Extensions;
using TestProject.Application.Common.Interfaces;
using TestProject.Application.Common.Models;
using TestProject.Domain.Entities;

namespace TestProject.Application.Users.Queries.ListUsers
{
    public class ListUsersQuery : IRequest<PaginatedList<User>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PaginatedList<User>>
        {
            private readonly IDatabaseContext context;

            public ListUsersQueryHandler(IDatabaseContext context)
            {
                this.context = context;
            }

            public async Task<PaginatedList<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
            {
                return await context.Users
                    .OrderBy(e => e.Id)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }

    }
}
