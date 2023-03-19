using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetPersonPurcashFoodQuery : IRequest<PersonPurcashDto>
    {
        public GetPersonPurcashFoodQuery(string pessoasId)
        {
            this.Id_Pessoas = pessoasId;
        }

        public string Id_Pessoas { get; set; }
    }
}
