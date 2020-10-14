
using TestProject.Domain.Entities;
using TestProject.Domain.Enums;

namespace TestProject.Application.Services
{
    public interface IAccountCreditScoreCalculator
    {
        CreditScore GetUserCreditScore(User user);
    }
}
