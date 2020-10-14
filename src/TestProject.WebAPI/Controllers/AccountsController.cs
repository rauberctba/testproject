using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestProject.Application.Accounts.Commands.CreateAccount;
using TestProject.Application.Accounts.Queries.ListAccounts;
using TestProject.Application.Users.Queries.GetUser;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountRequest request)
        {
            // TODO: Refactor - move this logic to the command handler class
            var user = await mediator.Send(new GetUserQuery { Id = request.UserId });
            if (user == null)
            {
                return BadRequest();
            }

            var account = await mediator.Send(new CreateAccountCommand { User = user });
            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListAccountsQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
