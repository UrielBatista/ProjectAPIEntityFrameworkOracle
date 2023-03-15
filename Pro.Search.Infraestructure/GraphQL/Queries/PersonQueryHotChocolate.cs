using HotChocolate;
using HotChocolate.Data;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.GraphQL.DataLoaders;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.GraphQL.Queries
{
    public class PersonQueryHotChocolate
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

        public async Task<List<PersonsAllInfoDto>> GetPerson(string id, PersonDataLoad dataLoader) => 
            await dataLoader.LoadAsync(id);
    }

}
