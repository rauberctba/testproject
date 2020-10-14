using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Application.Common.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
