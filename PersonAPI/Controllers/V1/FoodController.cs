using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// This method list all foods created in database.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(FoodResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListFoods([FromQuery] PageAndFilteredRequestParams parans)
        {
            var response = await mediator.Send(new GetAllFoodQuery(parans));
            if (response == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// This method create and persist food in database local or in oracle.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("/persistence")]
        [ProducesResponseType(typeof(FoodAllInfoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostFood([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new CreateFoodCommand(foodAllInfoDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        /// <summary>
        /// This method create food in memory database with redis.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("/inMemory")]
        [ProducesResponseType(typeof(FoodAllInfoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostFoodInMemory([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new CreateFoodInMemoryCommand(foodAllInfoDto)).ConfigureAwait(false);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        /// <summary>
        /// This method update food if already exist in database.
        /// </summary>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(FoodAllInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateFood([FromBody] FoodAllInfoDto foodAllInfoDto)
        {
            var response = await mediator.Send(new UpdateFoodCommand(foodAllInfoDto));
            if (response == null) return NoContent();
            return Ok(response);
        }

        /// <summary>
        /// This method delete food with param passed.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Food>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFood([FromQuery] string id_food)
        {
            var response = await mediator.Send(new DeleteFoodCommand(id_food)).ConfigureAwait(false);
            var valueResponse = response.Any();
            if (!valueResponse) return NoContent();
            return Ok(response);
        }
    }
}
