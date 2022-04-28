using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetListPersonsPurcashFoodsQueryHandler : IQueryHandler<GetListPersonsPurcashFoodsQuery, IEnumerable<PersonPurcashDto>>
    {
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public GetListPersonsPurcashFoodsQueryHandler(IPersonsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PersonPurcashDto>> Handle(GetListPersonsPurcashFoodsQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var requestAllPersonsAndFoods = await this.repository
                .FindListPersonsPurcashFoods(request.Id_Pessoas, cancellationToken)
                .ConfigureAwait(false);

            if (requestAllPersonsAndFoods == null) return null;

            var resultPersonPurcashDto = new List<PersonPurcashDto>();

            foreach (var item in requestAllPersonsAndFoods)
            {
                var data = new PersonPurcashDto
                {
                    Pessoas = this.mapper.Map<Persons, PersonsAllInfoDto>(item),
                    Food = this.mapper.Map<IEnumerable<Food>, IEnumerable<FoodAllInfoDto>>(item.ComidaComprada),
                };

                resultPersonPurcashDto.Add(data);
            }

            return resultPersonPurcashDto;
        }
    }
}
