using FluentAssertions;
using System.Collections.Generic;
using TestProject.Application.Services;
using TestProject.Domain.Entities;
using TestProject.Domain.Enums;
using Xunit;

namespace TestProject.Tests.Application.Services
{
    public class AccountCreditScoreCalculatorTests
    {
        public class GetUserCreditScoreMethod
        {
            [Theory]
            [MemberData(nameof(CreditScoreData))]
            public void ShouldReturnCorrectScore(decimal monthlySalary, decimal monthlyExpenses, CreditScore expectedScore)
            {
                var creditScoreCalculator = new AccountCreditScoreCalculator();
                var user = new User { MonthlySalary = monthlySalary, MonthlyExpenses = monthlyExpenses };

                var result = creditScoreCalculator.GetUserCreditScore(user);

                result.Should().Be(expectedScore);
            }

            [Fact]
            public void ShouldReturnUnknownWhenUserIsNull()
            {
                var creditScoreCalculator = new AccountCreditScoreCalculator();

                var result = creditScoreCalculator.GetUserCreditScore(null);

                result.Should().Be(CreditScore.Unknown);
            }

            public static IEnumerable<object[]> CreditScoreData =>
                new List<object[]>
                {
                    new object[] {  1000, 100, CreditScore.Excellent },
                    new object[] {  1000, 101, CreditScore.Good },
                    new object[] {  1000, 300, CreditScore.Good },
                    new object[] {  1000, 700, CreditScore.Fair },
                    new object[] {  1000, 701, CreditScore.Bad },
                };
        }
    }
}
