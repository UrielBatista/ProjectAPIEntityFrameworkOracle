using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.PersonCommands;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator mediator;

        public PessoasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPessoas()
        {
            var response = await mediator.Send(new GetAllPersonQuery());
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpGet("{id_pessoa}")]
        public async Task<IActionResult> BuscarUmaPessoa(string id)
        {
            var response = await mediator.Send(new GetOnePersonQuery(id));
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpGet("/media")]
        public async Task<IActionResult> CalcMedia()
        {
            var data = await mediator.Send(new GetMediaPersonQuery());
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> InserirPessoas([FromBody] PersonDto personDto)
        {
            var response = await mediator.Send(new CreatePersonCommand(personDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarPessoasAsync([FromBody] PersonDto command)
        {
            var response = await mediator.Send(new UpdatePersonCommand(command)).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id_pessoa}")]
        public async Task<IActionResult> DeletearPessoas(string id)
        {
            var response = await mediator.Send(new DeletePersonCommand(id)).ConfigureAwait(false);
            if (response.Count == 0) return NotFound();
            return Ok(response);
        }
    }
}
