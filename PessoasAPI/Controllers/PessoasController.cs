using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.PersonCommands.Queries.Requests;
using Pro.Search.PersonCommands.Queries.Responses;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers
{
    [ApiController]
    [Route("V1/Pessoas")]
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
            try
            {
                var response = await mediator.Send(new GetAllPersonQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUmaPessoa(string id)
        {
            try
            {
                var response = await mediator.Send(new GetOnePersonQuery(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("media")]
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
        [Route("{id}")]
        public async Task<IActionResult> DeletearPessoas(string id)
        {
            try
            {
                var response = await mediator.Send(new DeletePersonCommand(id)).ConfigureAwait(false);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
