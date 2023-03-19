using BuldBlocks.Domain.Commons;
using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonCommand : IRequest<CreateOrUpdateResponses>
    {
        public CreatePersonCommand(PersonDto dto)
        {
            this.PersonDto = dto;
        }

        public PersonDto PersonDto { get; set; }
    }
}
