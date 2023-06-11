using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pro.Search.PersonCommands;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PersonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// This method list all persons created in database.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonsAllInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListPersons()
        {
            var response = await mediator.Send(new GetAllPersonQuery());
            if (response == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// This method list all persons with linked foods.
        /// </summary>
        [HttpGet("ListPersonRequestFood")]
        [ProducesResponseType(typeof(List<PersonsAllInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListPersonsWithFood()
        {
            var response = await mediator.Send(new GetAllPersonWithFoodQuery());
            if (response == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// This method get a single person with id_pessoa param.
        /// </summary>
        [HttpGet("search-person")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchOnePerson([FromQuery] string id_pessoa)
        {
            var response = await mediator.Send(new GetOnePersonQuery(id_pessoa));
            if (response == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// This method calc media with Pessoas_Calc_Number of all persons.
        /// </summary>
        [HttpGet("media")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcMedia()
        {
            var data = await mediator.Send(new GetMediaPersonQuery());
            return Ok(data);
        }

        /// <summary>
        /// This method request service CepAPI and get address where pass cep param.
        /// </summary>
        [HttpGet("CepLocalization")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCep([FromQuery] string cep)
        {
            var data = await mediator.Send(new GetCepQuery(cep));
            return Ok(data);
        }

        /// <summary>
        /// This method create person and persist in database.
        /// </summary>
        [HttpPost]
        // [Authorize]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPersons([FromBody] PersonDto personDto)
        {
            var response = await mediator.Send(new CreatePersonCommand(personDto)).ConfigureAwait(false);

            return response.Match<ActionResult>(
                success => this.Ok(success.PersonDto),
                notFound => this.NotFound(notFound.Message),
                badRequest => this.BadRequest(badRequest.Message));
        }

        /// <summary>
        /// This method update person if already exist in database.
        /// </summary>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDto command)
        {
            var response = await mediator.Send(new UpdatePersonCommand(command)).ConfigureAwait(false);

            return response.Match<ActionResult>(
                success => this.Ok(success.PersonDto),
                notFound => this.NotFound(notFound.Message),
                badRequest => this.BadRequest(badRequest.Message));
        }

        /// <summary>
        /// This method delete person if not linked food and already exist in database with pass
        /// param id_pessoa.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(typeof(List<Persons>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePerson([FromQuery] string id_pessoa)
        {
            var response = await mediator.Send(new DeletePersonCommand(id_pessoa)).ConfigureAwait(false);
            
            return response.Match<ActionResult>(
                success => this.Ok(success.Persons),
                badRequest => this.BadRequest(badRequest.Message));
        }
    }
}
