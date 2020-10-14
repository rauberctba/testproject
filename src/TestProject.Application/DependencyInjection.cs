using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestProject.Application.Common.Behaviours;
using TestProject.Application.Services;

namespace TestProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                .AddTransient<IAccountCreditScoreCalculator, AccountCreditScoreCalculator>()
                .AddTransient<IAccountCreditCheck, AccountCreditCheck>();

            return services;
        }
    }
}
