using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> FindAllAsyncFood(CancellationToken cancellationToken);

        Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken);
    }
}
