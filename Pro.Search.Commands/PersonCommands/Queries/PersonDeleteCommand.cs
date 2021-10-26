using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonDeleteCommand : ICommand<PersonDto>
    {
        public PersonDeleteCommand(string Id_Pessoas)
        {
            this.Id_Pessoas = Id_Pessoas;
        }

        public string Id_Pessoas { get; set; }
    }
}
