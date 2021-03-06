﻿using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Application.Users.CreateUser;
using TestProject.Domain.Entities;
using Xunit;

namespace TestProject.Tests.Users.Commands
{
    public class CreateUserTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture fixture;

        public CreateUserTests(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Theory]
        [MemberData(nameof(CreateUserCommandInvalidData))]
        public void ShouldThrowValidationExceptionWhenCommandHasInvalidProperties(CreateUserCommand command)
        {
            FluentActions.Invoking(() => fixture.SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenEmailIsNotUnique()
        {
            var command = new CreateUserCommand { Email = "valid@email.com", Name = "Valid name", MonthlyExpenses = 100, MonthlySalary = 1000 };
            await fixture.SendAsync(command);

            FluentActions.Invoking(() => fixture.SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Fact]
        public async Task ShouldPersistAndReturnAUser()
        {
            var command = new CreateUserCommand
            {
                Email = "fake@email.com",
                MonthlyExpenses = 50,
                MonthlySalary = 400,
                Name = "Fake user"
            };

            var response = await fixture.SendAsync(command);

            var expectedUser = new User
            {
                Id = response.Id,
                Email = command.Email,
                MonthlyExpenses = command.MonthlyExpenses,
                MonthlySalary = command.MonthlySalary,
                Name = command.Name
            };

            response.Should().BeEquivalentTo(expectedUser);
        }


        public static IEnumerable<object[]> CreateUserCommandInvalidData =>
            new List<object[]>
            {
                // TODO: Use AutoFixture
                new object[] { new  CreateUserCommand() },
                new object[] { new CreateUserCommand { Email="invalidemail", MonthlyExpenses=10, MonthlySalary=10, Name="Valid name" } },
                new object[] { new CreateUserCommand { Email=null, MonthlyExpenses=10, MonthlySalary=10, Name="Valid name" } },
                new object[] { new CreateUserCommand { Email="valid@email.com", MonthlyExpenses=0, MonthlySalary=10, Name="Valid name" } },
                new object[] { new CreateUserCommand { Email="valid@email.com", MonthlyExpenses=5, MonthlySalary=0, Name="Valid name" } },
                new object[] { new CreateUserCommand { Email="valid@email.com", MonthlyExpenses=6, MonthlySalary=10, Name=null } },
            };
    }
}
