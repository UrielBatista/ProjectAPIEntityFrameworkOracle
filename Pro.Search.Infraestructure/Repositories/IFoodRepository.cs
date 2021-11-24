using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> FindAllAsyncFood(CancellationToken cancellationToken);

        IEnumerable<Food> FindAllAsyncFoodReferenceToPerson(string Id_Pessoas);

        Task<Food> FindOneAsyncFoodReferenceToPerson(string Id_Pessoas, CancellationToken cancellationToken);
        
        Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken);
    }
}
