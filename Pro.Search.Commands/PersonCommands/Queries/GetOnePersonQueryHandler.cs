using AutoMapper;
using MediatR;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetOnePersonQueryHandler : IRequestHandler<GetOnePersonQuery, PersonDto>
    {
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public GetOnePersonQueryHandler(IPersonsRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetOnePersonQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            
            var personDb = await this.repository.FindOneAsyncPerson(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);
            
            if (personDb == null) return null;
            
            var personDto = new PersonDto
            {
                Pessoas = this.mapper.Map<Persons, PersonsInfoDto>(personDb),
            };

            return personDto;
        }
    }
}
