using FluentValidation;
using TestProject.Application.Common.Constants;

namespace TestProject.Application.Accounts.Queries.ListAccounts
{
    public class ListAccountsQueryValidator : AbstractValidator<ListAccountsQuery>
    {
        public ListAccountsQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThan(0);

            RuleFor(v => v.PageSize)
                .InclusiveBetween(1, PaginationConstants.MaxPageSize);
        }
    }
}
