using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class PersonsPickerController : ControllerBase
    {
        private readonly IMediator mediator;

        public PersonsPickerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PersonPickerDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromQuery] IEnumerable<string> persons)
        {
            var response = await mediator.Send(new CreatePersonPickerCommand(persons)).ConfigureAwait(false);

            return response.Match<ActionResult>(
                success => this.Ok(success.Persons));
        }
    }
}
