﻿using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQuery : IQuery<FoodResponse>
    {
        public GetAllFoodQuery(PageAndFilteredRequestParams parans)
        {
            this.Page = parans.PageNumber;
            this.PageSize = parans.PageSize;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
