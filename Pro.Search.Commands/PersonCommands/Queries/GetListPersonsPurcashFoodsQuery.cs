using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Collections.Generic;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetListPersonsPurcashFoodsQuery : IQuery<IEnumerable<PersonPurcashDto>>
    {
        public GetListPersonsPurcashFoodsQuery(string[] pessoasId)
        {
            this.Id_Pessoas = pessoasId;
        }

        public string[] Id_Pessoas { get; set; }
    }
}
