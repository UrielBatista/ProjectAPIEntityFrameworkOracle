using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetOnePersonQuery : IRequest<PersonDto>
    {
        public GetOnePersonQuery(string pessoasId)
        {
            this.Id_Pessoas = pessoasId;
        }

        public string Id_Pessoas { get; set; }
    }
}
