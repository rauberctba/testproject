using Microsoft.Extensions.Configuration;
using TestProject.Application.Common.Constants;
using TestProject.Domain.Entities;

namespace TestProject.Application.Services
{
    public class AccountCreditCheck : IAccountCreditCheck
    {
        private readonly decimal creditLimit;

        public AccountCreditCheck(IConfiguration configuration)
        {
            creditLimit = configuration.GetValue("AccountCreditLimit", AccountConstants.DefaultAccountCreditLimit);
        }

        public bool CheckUserCredit(User user)
        {
            if (user == null)
            {
                return false;
            }

            return user.MonthlySalary - user.MonthlyExpenses >= creditLimit;
        }
    }
}
