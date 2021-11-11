using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands.Queries;
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
    }
}
