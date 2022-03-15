using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> FindAllAsyncFood(int page, int pageSize, bool flagsCancellationToken, CancellationToken cancellationToken);

        Task<IEnumerable<Food>> FindAllAsyncFoodReferenceToPerson(string Id_Pessoas, CancellationToken cancellationToken);

        Task<Food> FindOneAsyncFoodReferenceToPerson(string Id_Pessoas, CancellationToken cancellationToken);
        
        Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken);

        Task<List<Food>> FindListFoodReferenceToIDFood(string result, CancellationToken cancellationToken);
    }
}
