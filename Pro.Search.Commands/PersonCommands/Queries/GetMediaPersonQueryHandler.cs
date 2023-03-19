using MediatR;
using Pro.Search.Infraestructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetMediaPersonQueryHandler : IRequestHandler<GetMediaPersonQuery, decimal>
    {
        private readonly IPersonsRepository repository;

        public GetMediaPersonQueryHandler(IPersonsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<decimal> Handle(GetMediaPersonQuery request, CancellationToken cancellationToken)
        {
            var personsAllDb = await this.repository.CalcMediaPersonNumber(cancellationToken);

            decimal total = personsAllDb.Sum();
            total = total / personsAllDb.Count;
            return total;
        }
    }
}
