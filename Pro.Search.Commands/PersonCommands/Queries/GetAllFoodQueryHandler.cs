using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQueryHandler : IQueryHandler<GetAllFoodQuery, List<FoodDto>>
    {
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public GetAllFoodQueryHandler(IFoodRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<FoodDto>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            var foodAllDb = await this.repository.FindAllAsyncFood(cancellationToken);

            var data = new List<FoodDto>();
            foreach (var i in foodAllDb)
            {
                data.Add(mapper.Map<Food, FoodDto>(i));
            }

            return data;
        }
    }
}
