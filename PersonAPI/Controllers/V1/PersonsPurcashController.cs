using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsPurcashController : ControllerBase
    {
        private readonly IMediator mediator;

        public PersonsPurcashController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PersonPurcashDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PersonsPurcashedFood(string id_pessoa)
        {
            var response = await mediator.Send(new GetPersonPurcashFoodQuery(id_pessoa));
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(PersonPurcashDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PersonsPurcashedFood(string id_pessoa, [FromBody] PersonPurcashDto personPurcashDto)
        {
            var response = await mediator.Send(new SetPessoasPurcashCommand(id_pessoa, personPurcashDto));
            if (response == null) return NotFound();
            return Ok(response);
        }
    }
}
