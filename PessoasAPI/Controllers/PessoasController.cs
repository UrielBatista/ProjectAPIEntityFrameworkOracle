using Microsoft.AspNetCore.Mvc;
using PessoasAPI.Context;
using PessoasAPI.Model;
using PessoasAPI.Repository;
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

        /*[HttpPost]
        [Route("CriarPessoas")]
        public Task<CreatePessoasResponse> Create (
            [FromServices] IMediator mediator, [FromBody] CreatePessoasRequest command)
        {
            return mediator.Send(command);
        }*/


        [HttpGet]
        public async Task<IActionResult> ListarPessoas()
        {
            try
            {
                var data = await _pessoasRepository.ListarPessoasAsync();
                if (data.Count == 0)
                {
                    return StatusCode(404, new { message = "Dados não encontrados!" });
                }
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("media")]
        public async Task<IActionResult> CalcMedia()
        {
            try
            {
                var data =  _pessoasRepository.CalcMediaAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InserirPessoas([FromBody] Pessoas pessoas)
        {
            try
            {
                var data = await _pessoasRepository.InsertPessoasAsync(pessoas);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message });

            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarPessoasAsync([FromBody] Pessoas pessoas)
        {
            var data = await _pessoasRepository.AtualizarPessoasAsync(pessoas);
            return Ok(data);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletearPessoas(string id)
        {
            var retorno = await _pessoasRepository.ListarPessoaUnicaAsync(id);
                
            if (retorno != null)
            {
                _pessoasRepository.DeletePessoasAsync(retorno);
            }
            return Ok(retorno);
        }
    }
}
