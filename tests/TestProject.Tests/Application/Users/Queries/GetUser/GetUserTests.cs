using FluentAssertions;
using System;
using System.Threading.Tasks;
using TestProject.Application.Users.CreateUser;
using TestProject.Application.Users.Queries.GetUser;
using Xunit;

namespace TestProject.Tests.Application.Users.Queries.GetUser
{
    public class GetUserTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture fixture;

        public GetUserTests(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task ShouldReturnNullWhenUserNotExist()
        {
            var query = new GetUserQuery { Id = 999 };
            var result = await fixture.SendAsync(query);

            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnUserWhenExist()
        {
            var createUserCommand = new CreateUserCommand
            {
                Email = "fake@email.me",
                MonthlyExpenses = 123,
                MonthlySalary = 456,
                Name = "fake user name"
            };
            var persistedUser = await fixture.SendAsync(createUserCommand);

            var query = new GetUserQuery { Id = persistedUser.Id };
            var result = await fixture.SendAsync(query);

            result.Should().BeEquivalentTo(persistedUser);
        }
    }
}
