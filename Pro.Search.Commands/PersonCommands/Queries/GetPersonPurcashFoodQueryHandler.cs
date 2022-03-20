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
    public class GetPersonPurcashFoodQueryHandler : IQueryHandler<GetPersonPurcashFoodQuery, PersonPurcashDto>
    {
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public GetPersonPurcashFoodQueryHandler(IPersonsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PersonPurcashDto> Handle(GetPersonPurcashFoodQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var personPurcashFood = await this.repository.FindPersonPurcashFood(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);

            if (personPurcashFood == null) return null;

            var returnPersonPurcashFood = new PersonPurcashDto
            {
                Pessoas = this.mapper.Map<Persons, PersonsAllInfoDto>(personPurcashFood),
                Food = this.mapper.Map<IEnumerable<Food>, IEnumerable<FoodAllInfoDto>>(personPurcashFood.ComidaComprada)
            };

            return returnPersonPurcashFood;


        }
    }
}
