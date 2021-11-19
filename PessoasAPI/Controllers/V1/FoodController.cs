using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands.Queries;
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
        public async Task<IActionResult> ListarComida()
        {
            var response = await mediator.Send(new GetAllFoodQuery());
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InserirComida([FromBody] FoodDto foodDto)
        {
            var response = await mediator.Send(new CreateFoodCommand(foodDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarComida([FromBody] FoodDto foodDto)
        {
            var response = await mediator.Send(new UpdateFoodCommand(foodDto));
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
