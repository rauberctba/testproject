using FluentAssertions;
using FluentValidation;
using TestProject.Application.Users.CreateUser;
using Xunit;

namespace TestProject.Tests.Users.Commands
{
    public class CreateUserTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture fixture;

        public CreateUserTests(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldThrowValidationExceptionWhenValidationFails()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(() => fixture.SendAsync(command)).Should().Throw<ValidationException>();
        }
    }
}
