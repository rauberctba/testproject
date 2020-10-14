using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TestProject.Application.Services;
using TestProject.Domain.Entities;
using Xunit;

namespace TestProject.Tests.Application.Services
{
    public class AccountCreditCheckTests
    {
        public class CheckUserCreditMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenUserIsNull()
            {
                var builder = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "AccountCreditLimit", "500" }
                    });

                var creditCheck = new AccountCreditCheck(builder.Build());

                var result = creditCheck.CheckUserCredit(null);
                result.Should().BeFalse();
            }

            [Theory]
            [MemberData(nameof(CreditCheckData))]
            public void ShouldReturnCorrectResult(decimal monthlySalary, decimal monthlyExpenses, bool expectedResult) 
            {
                var builder = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "AccountCreditLimit", "500" }
                    });

                var creditCheck = new AccountCreditCheck(builder.Build());
                var user = new User { MonthlySalary = monthlySalary, MonthlyExpenses = monthlyExpenses };

                var result = creditCheck.CheckUserCredit(user);

                result.Should().Be(expectedResult);
            }

            public static IEnumerable<object[]> CreditCheckData =>
                new List<object[]>
                {
                    new object[] {  5000, 4000, true },
                    new object[] {  1000, 500, true },
                    new object[] {  1000, 501, false },
                };
        }
    }
}
