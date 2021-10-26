using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonCreateCommand : ICommand<PersonDto>
    {
        public PersonCreateCommand(PersonDto dto)
        {
            this.PersonDto = dto;
        }

        public PersonDto PersonDto { get; set; }
    }
}
