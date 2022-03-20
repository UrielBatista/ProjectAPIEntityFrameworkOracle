using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetAllPersonWithFoodQueryHandler : IQueryHandler<GetAllPersonWithFoodQuery, List<PersonsAllInfoDto>>
    {
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public GetAllPersonWithFoodQueryHandler(IPersonsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PersonsAllInfoDto>> Handle(GetAllPersonWithFoodQuery request, CancellationToken cancellationToken)
        {
            var resultPessoa = await this.repository.FindAsyncPessoaWithFood(cancellationToken).ConfigureAwait(false);

            if (resultPessoa == null)
                return null;

            var resultDto = new List<PersonsAllInfoDto>();
            foreach (var i in resultPessoa)
            {
                resultDto.Add(mapper.Map<Persons, PersonsAllInfoDto>(i));
            }

            return resultDto;

        }
    }
}
