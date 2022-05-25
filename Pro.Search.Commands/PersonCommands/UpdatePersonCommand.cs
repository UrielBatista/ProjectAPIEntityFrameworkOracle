using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;

namespace Pro.Search.PersonCommands
{
    public class UpdatePersonCommand : ICommand<CreateOrUpdateResponses>
    {
        public UpdatePersonCommand(PersonDto dto)
        {
            this.PersonDto = dto;
        }

        public PersonDto PersonDto { get; set; }
    }
}
