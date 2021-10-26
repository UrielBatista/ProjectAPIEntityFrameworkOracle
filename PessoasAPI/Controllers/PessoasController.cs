using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.PersonDomains.PersonEngine.Commands;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Queries;
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


        //[HttpGet]
        //[Route("media")]
        //public async Task<IActionResult> CalcMedia()
        //{
        //    try
        //    {
        //        var data =  await _pessoasRepository.CalcMediaAsync();
        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(400, new { message = ex.Message });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> InserirPessoas([FromBody] PersonDto personDto)
        {
            try
            {
                var response = await mediator.Send(new PersonCreateCommand(personDto)).ConfigureAwait(false);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> AtualizarPessoasAsync(
        //    [FromServices] IMediator mediator, 
        //    [FromBody] PersonUpdateCommand command)
        //{
        //    var response = await mediator.Send(command);
        //    return Ok(response);
        //}

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletearPessoas(string id)
        {
            try
            {
                var response = await mediator.Send(new PersonDeleteCommand(id)).ConfigureAwait(false);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
