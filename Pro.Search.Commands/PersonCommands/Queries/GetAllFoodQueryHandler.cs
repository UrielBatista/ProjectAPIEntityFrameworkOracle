using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQueryHandler : IQueryHandler<GetAllFoodQuery, List<FoodAllInfoDto>>
    {
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public GetAllFoodQueryHandler(IPessoasRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<FoodAllInfoDto>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            var foodAllDb = await this.repository.FindAllAsyncFood(cancellationToken);

            var data = new List<FoodAllInfoDto>();
            foreach (var i in foodAllDb)
            {
                data.Add(mapper.Map<Food, FoodAllInfoDto>(i));
            }

            return data;
        }
    }
}
