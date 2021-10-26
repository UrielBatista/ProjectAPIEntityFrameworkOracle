using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonDomains.PersonEngine.Queries
{
    public class GetOnePersonQuery : IQuery<PersonDto>
    {
        public GetOnePersonQuery(string pessoasId)
        {
            this.Id_Pessoas = pessoasId;
        }

        public string Id_Pessoas { get; set; }
    }

    
}
