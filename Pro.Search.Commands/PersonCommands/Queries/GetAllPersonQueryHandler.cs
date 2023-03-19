using AutoMapper;
using MediatR;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, List<PersonsAllInfoDto>>
    {
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public GetAllPersonQueryHandler(IPersonsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PersonsAllInfoDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {
            var personsAllDb = await this.repository.FindAllAsyncPerson(cancellationToken);

            var data = new List<PersonsAllInfoDto>();
            foreach(var i in personsAllDb)
            {
                data.Add(mapper.Map<Persons, PersonsAllInfoDto>(i));
            }

            return data;
        }
    }
}
