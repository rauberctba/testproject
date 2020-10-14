using FluentValidation;
using TestProject.Application.Common.Constants;

namespace TestProject.Application.Users.Queries.ListUsers
{
    public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
    {
        public ListUsersQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThan(0);

            RuleFor(v => v.PageSize)
                .InclusiveBetween(1, PaginationConstants.MaxPageSize);
        }
    }
}
