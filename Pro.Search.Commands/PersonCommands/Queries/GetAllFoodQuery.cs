using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class GetAllFoodQuery : IRequest<FoodResponse>
    {
        public GetAllFoodQuery(PageAndFilteredRequestParams parans)
        {
            this.Page = parans.PageNumber;
            this.PageSize = parans.PageSize;
            this.FlagsValue = parans.flagsValue;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool FlagsValue { get; set; }
    }
}
