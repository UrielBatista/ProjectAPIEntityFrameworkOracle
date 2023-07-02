using HotChocolate;
using HotChocolate.Data;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.GraphQL.DataLoaders;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
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
        public IQueryable<Persons> GetPersonsData([Service] ISystemWriteDBContext context)
        {
            var queryable = context.Pessoas;
            DisposeIfDisposable(ref queryable);
            return queryable;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Food> GetFoodPurchase([Service] ISystemWriteDBContext context)
        {
            var result = context.Food;
            DisposeIfDisposable(ref result);
            return result;
        }

        public async Task<List<PersonsAllInfoDto>> GetPerson(string id, PersonDataLoad dataLoader) =>
            await dataLoader.LoadAsync(id);

        public async Task<ValidityFlagDto> ValidFlagsPersonExist(RequestIdsDto requestIds,
            ValidityFlagPersonExistDataLoad dataLoader) =>
            await dataLoader.LoadAsync(requestIds);

        public static void DisposeIfDisposable<T>(ref T obj)
        {
            if (obj is IDisposable disposable)
            {
                disposable.Dispose();
                obj = default(T);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
