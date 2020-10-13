using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using TestProject.WebAPI;

namespace TestProject.Tests
{
    public class IntegrationTestsFixture
    {
        private IConfigurationRoot configuration;
        private IServiceScopeFactory scopeFactory;

        public IntegrationTestsFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            configuration = builder.Build();
            var services = new ServiceCollection();
            var startup = new Startup(configuration);

            startup.ConfigureServices(services);

            scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

    }
}
