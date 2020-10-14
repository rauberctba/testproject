using TestProject.Domain.Entities;
using TestProject.Domain.Enums;

namespace TestProject.Application.Services
{
    public class AccountCreditScoreCalculator : IAccountCreditScoreCalculator
    {
        public CreditScore GetUserCreditScore(User user)
        {
            if (user == null)
            {
                return CreditScore.Unknown;
            }


            var expensesPercentage = (user.MonthlyExpenses / user.MonthlySalary) * 100;

            // Values hard-coded for simplicity but we should consider inject using configuration
            // or injecting multiple rules to define the credit score.
            if (expensesPercentage <= 10)
            {
                return CreditScore.Excellent;
            }

            if (expensesPercentage <= 30)
            {
                return CreditScore.Good;
            }

            if (expensesPercentage <= 70)
            {
                return CreditScore.Fair;
            }

            return CreditScore.Bad;
        }
    }
}
