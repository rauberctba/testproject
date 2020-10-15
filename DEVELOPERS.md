# User API Dev Guide

The solution is implemented following Clean Architecture principles and is split into Domain, Application, WebAPI, and Infrastructure layer.

Business logic is inside the Application layer and implemented using CQRS with Mediator and all the code for a feature is inside the same folder. So, if you are looking to create a user go to Users\Commands\CreateUser - the command, handler and validations are in the same place.

Validation logic is implemented using FluentValidation, it's dynamically loaded during initialization is it's applied to Commands and Queries using a Mediator PipelineBehavior.

Persistence is part of the infrastructure layer and the implementation is based on EF Core. Since EF uses providers to abstract the underlying database technology no further abstraction was created as part of this test, but this means that the application depends on ef library. A repository pattern could be implemented to provide further abstraction between the application and ef library.

## Building

VS can be used for local development but a docker image can be build for production deployments using the following command:
docker build -t testproject:webapi .

## Testing

In order to demonstrate different test approaches, I have created 2 different types of tests, unit tests, and integration tests.

- Unit tests are used to test a few services and extensions, look at tests\Application\Services
- Integration tests are used to cover features like the CreateUserCommand or GetUserQuery using EF InMemory provider. This strategy makes sure that the application business works properly as intended, including validations and persistence.

\*\* I haven't covered all classes with tests due to time to finish in the recommended 3-4hours on the test but the 2 practices can be applied to any class on this solution.

\*\*\* When possible I was using xUnit [Theory] to cover different test scenarios using the same logic.

## Deploying

A dockerfile is provided as part of this project and the result can be used as a production container. The docker file provided can be further optimized to take advantage of docker layer caching by copying the csproj files individually before running dotnet restore.

Database can be updated using the following command:
dotnet ef database update --project src\TestProject.Infrastructure --startup-project src\TestProject.WebAPI

## Additional Information

Using CQRS in a small application is probably over-engineering but since we talked about CQRS and event-driven applications I thought that it was a good idea.

This application doesn't emit events, but the handlers are a perfect place to emit them.
