using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IMediator mediator;

        public FoodController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarComida([FromQuery] PageAndFilteredRequestParams @params)
        {
            var response = await mediator.Send(new GetAllFoodQuery(@params.PageNumber, @params.PageSize));
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        [Route("/persistence")]
        public async Task<IActionResult> InserirComida([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new CreateFoodCommand(foodAllInfoDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpPost]
        [Route("/inMemory")]
        public async Task<IActionResult> InserirComidaEmMemoria([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new CreateFoodInMemoryCommand(foodAllInfoDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarComida([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new UpdateFoodCommand(foodAllInfoDto));
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id_food}")]
        public async Task<IActionResult> DeletearComida(string id_food)
        {
            var response = await mediator.Send(new DeleteFoodCommand(id_food)).ConfigureAwait(false);
            if (response.Count == 0) return NotFound();
            return Ok(response);
        }
    }
}
