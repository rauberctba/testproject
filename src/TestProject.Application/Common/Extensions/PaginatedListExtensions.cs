using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Application.Common.Models;

namespace TestProject.Application.Common.Extensions
{
    public static class PaginatedListExtensions
    {
        public static async Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            var totalCount = await queryable.CountAsync();
            var items = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TDestination>(items, totalCount, pageNumber, pageSize);
        }
    }
}
