using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Application.Common.Extensions;
using TestProject.Application.Common.Interfaces;
using TestProject.Application.Common.Models;
using TestProject.Domain.Entities;

namespace TestProject.Application.Accounts.Queries.ListAccounts
{
    public class ListAccountsQuery : IRequest<PaginatedList<Account>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public class ListAccountsQueryHandler : IRequestHandler<ListAccountsQuery, PaginatedList<Account>>
        {
            private readonly IDatabaseContext context;

            public ListAccountsQueryHandler(IDatabaseContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<PaginatedList<Account>> Handle(ListAccountsQuery request, CancellationToken cancellationToken)
            {
                return await context.Accounts
                    .OrderBy(e => e.Id)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}
