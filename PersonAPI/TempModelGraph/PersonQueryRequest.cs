using HotChocolate;
using HotChocolate.Data;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Linq;

namespace PersonAPI.TempModelGraph
{
    public class PersonQueryRequest
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Persons> GetPersonsData([Service] ISystemDBContext context) =>
            context.Pessoas;

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Food> GetFoodPurchase([Service] ISystemDBContext context) =>
            context.Food;
    }

}
