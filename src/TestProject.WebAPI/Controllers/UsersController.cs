using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestProject.Application.Users.CreateUser;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var user = await mediator.Send(command);

            return Created($"/api/users/{user.Id}", user);
        }
    }
}
