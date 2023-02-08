using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonAPI.Controllers.V3
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PersonController : ODataController
    {
        private readonly IMediator mediator;

        public PersonController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// This method is endpoint to request elements of schema with OData
        /// </summary>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(List<PersonsAllInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListPersons()
        {
            var response = await mediator.Send(new GetAllPersonQuery());
            if (response == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// This method authentication endpoint, passing username and token generate token
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] TokenLoginRequestDto request)
        {
            var response = await mediator
                .Send(new ApplyTokenQuerySearhInDatabase(request));

            return response.Match<ActionResult>(
                success => this.Ok(success.TokenResponseDto),
                notFound => this.NotFound(notFound.Message));
        }
    }
}
