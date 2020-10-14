

using TestProject.Domain.Entities;

namespace TestProject.Application.Services
{
    public interface IAccountCreditCheck
    {
        bool CheckUserCredit(User user);
    }
}
