using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQuery : IQuery<FoodResponse>
    {
        public GetAllFoodQuery(int page, decimal pageSize)
        {
            this.Page = page;
            this.PageSize = pageSize;
        }

        public int Page { get; set; }

        public decimal PageSize { get; set; }
    }
}
