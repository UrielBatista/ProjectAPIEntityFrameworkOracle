using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries.Responses
{
    public class GetMediaPersonQueryHandler : IQueryHandler<GetMediaPersonQuery, float>
    {
        private readonly IPessoasRepository repository;

        public GetMediaPersonQueryHandler(IPessoasRepository repository)
        {
            this.repository = repository;
        }

        public async Task<float> Handle(GetMediaPersonQuery request, CancellationToken cancellationToken)
        {
            var personsAllDb = await this.repository.FindAllAsyncPerson(cancellationToken);

            List<float> mediaPerson = new List<float>();
            foreach (var item in personsAllDb)
            {
                mediaPerson.Add(item.Pessoas_Calc_Number);
            }
            float total = mediaPerson.Sum();
            total = total / personsAllDb.Count;
            return total;
        }
    }
}
