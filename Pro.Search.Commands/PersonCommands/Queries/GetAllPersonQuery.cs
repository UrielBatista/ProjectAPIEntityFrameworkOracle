using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class GetAllPersonQuery : IQuery<List<PessoasAllInfoDto>>
    {
        public GetAllPersonQuery() { }
    }
}
