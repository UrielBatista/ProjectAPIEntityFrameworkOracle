using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetAllPersonQueryHandler : IQueryHandler<GetAllPersonQuery, List<PessoasAllInfoDto>>
    {
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public GetAllPersonQueryHandler(IPessoasRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PessoasAllInfoDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {
            var personsAllDb = await this.repository.FindAllAsyncPerson(cancellationToken);

            var data = new List<PessoasAllInfoDto>();
            foreach(var i in personsAllDb)
            {
                data.Add(mapper.Map<Pessoas, PessoasAllInfoDto>(i));
            }

            return data;
        }
    }
}
