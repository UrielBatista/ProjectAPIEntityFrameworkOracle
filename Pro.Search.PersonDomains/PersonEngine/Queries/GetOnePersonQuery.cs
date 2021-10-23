using MediatR;
using PessoasAPI.Model;

namespace Pro.Search.PersonDomains.PersonEngine.Queries
{
    public class GetOnePersonQuery : IRequest<Pessoas>
    {
        public string Id_Pessoas { get; set; }
    }
}
