﻿using AutoMapper;
using MediatR;
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
    public class GetAllFoodQueryHandler : IRequestHandler<GetAllFoodQuery, FoodResponse>
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
            var allFoodsOrdened = await this.repository.FindAllAsyncFood(request.Page, request.PageSize, request.FlagsValue, cancellationToken);

            var pageCount = Math.Ceiling((double)allFoodsOrdened.Count() / (double)request.PageSize);

            var allFoods = new List<FoodAllInfoDto>();

            foreach (var i in allFoodsOrdened)
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
