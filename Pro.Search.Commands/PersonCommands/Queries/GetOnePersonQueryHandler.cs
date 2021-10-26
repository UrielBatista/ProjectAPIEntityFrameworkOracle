using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Dtos;
using Pro.Search.Infraestructure.Entities;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Queries
{
    public class GetOnePersonQueryHandler : IQueryHandler<GetOnePersonQuery, PersonDto>
    {
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public GetOnePersonQueryHandler(IPessoasRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetOnePersonQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            
            var personDb = await this.repository.FindOneAsyncPerson(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);
            var personDto = new PersonDto
            {
                Pessoas = this.mapper.Map<Pessoas, PessoasInfoDto>(personDb),
            };
            return personDto;
        }
    }
}
