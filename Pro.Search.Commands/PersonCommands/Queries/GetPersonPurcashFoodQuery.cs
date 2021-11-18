using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetPersonPurcashFoodQuery : IQuery<PersonPurcashDto>
    {
        public GetPersonPurcashFoodQuery(string pessoasId)
        {
            this.Id_Pessoas = pessoasId;
        }

        public string Id_Pessoas { get; set; }
    }
}
