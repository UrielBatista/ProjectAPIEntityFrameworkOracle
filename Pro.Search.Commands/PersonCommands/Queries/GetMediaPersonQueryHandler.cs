using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetMediaPersonQueryHandler : IQueryHandler<GetMediaPersonQuery, decimal>
    {
        private readonly IPessoasRepository repository;

        public GetMediaPersonQueryHandler(IPessoasRepository repository)
        {
            this.repository = repository;
        }

        public async Task<decimal> Handle(GetMediaPersonQuery request, CancellationToken cancellationToken)
        {
            var personsAllDb = await this.repository.FindAllAsyncPerson(cancellationToken);

            List<decimal> mediaPerson = new List<decimal>();
            foreach (var item in personsAllDb)
            {
                mediaPerson.Add(item.Pessoas_Calc_Number);
            }
            decimal total = mediaPerson.Sum();
            total = total / personsAllDb.Count;
            return total;
        }
    }
}
