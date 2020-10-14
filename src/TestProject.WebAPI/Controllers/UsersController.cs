using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestProject.Application.Users.CreateUser;
using TestProject.Application.Users.Queries.GetUser;
using TestProject.Application.Users.Queries.ListUsers;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var user = await mediator.Send(command);

            return Created($"/api/users/{user.Id}", user);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListUsersQuery query)
        {
            var pagedList = await mediator.Send(query);
            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await mediator.Send(new GetUserQuery { Id = id });

            return user != null 
                ? Ok(user) 
                : (IActionResult)NotFound();
        }
    }
}
