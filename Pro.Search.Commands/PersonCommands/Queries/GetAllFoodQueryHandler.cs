using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQueryHandler : IQueryHandler<GetAllFoodQuery, FoodResponse>
    {
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public GetAllFoodQueryHandler(IFoodRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<FoodResponse> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            var foodAllDb = await this.repository.FindAllAsyncFood(cancellationToken);

           
            var pageResult = request.PageSize;
            var pageCount = Math.Ceiling(foodAllDb.Count() / pageResult);


            var pagePickup = foodAllDb
                .Skip((request.Page - 1) * (int)pageResult)
                .Take((int)pageResult).ToList();


            var allFoods = new List<FoodAllInfoDto>();

            foreach (var i in pagePickup)
            {
                allFoods.Add(mapper.Map<Food, FoodAllInfoDto>(i));
            }

            var response = new FoodResponse
            {
                Foods = allFoods,
                CurrentPage = request.Page,
                Pages = (int)pageCount
            };

            return response;
        }
    }
}
