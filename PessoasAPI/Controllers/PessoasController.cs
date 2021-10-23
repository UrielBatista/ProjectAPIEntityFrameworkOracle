using MediatR;
using Microsoft.AspNetCore.Mvc;
using PessoasAPI.Context;
using PessoasAPI.Model;
using PessoasAPI.Repository;
using Pro.Search.PersonDomains.PersonEngine.Commands;
using Pro.Search.PersonDomains.PersonEngine.Queries;
using System;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers
{
    [ApiController]
    [Route("V1/Pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoasRepository _pessoasRepository;

        public PessoasController(IPessoasRepository pessoasRepository, ContextDB context)
        {
            _pessoasRepository = new PessoasRepository(context);

        }

        [HttpGet]
        public async Task<IActionResult> ListarPessoas([FromServices] IMediator mediator)
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
        public async Task<IActionResult> BuscarUmaPessoa(
            [FromServices] IMediator mediator, string id)
        {
            try
            {
                var response = await mediator.Send(new GetOnePersonQuery { Id_Pessoas = id });
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
            try
            {
                var data =  await _pessoasRepository.CalcMediaAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InserirPessoas(
            [FromBody] PersonCreateCommand command,
            [FromServices] IMediator mediator)
        {
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> AtualizarPessoasAsync(
            [FromServices] IMediator mediator, 
            [FromBody] PersonUpdateCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletearPessoas(
            [FromServices] IMediator mediator, string id)
        {
            try
            {
                var response = await mediator.Send(new PersonDeleteCommand { Id_Pessoas = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
