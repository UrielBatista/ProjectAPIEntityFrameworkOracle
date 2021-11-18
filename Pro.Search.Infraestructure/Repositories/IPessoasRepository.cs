using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories
{
    public interface IPessoasRepository
    {
        Task<Pessoas> FindPersonPurcashFood(string Id_Pessoas, CancellationToken cancellationToken);

        Task<Pessoas> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken);
        
        Task<List<Pessoas>> FindAllAsyncPerson(CancellationToken cancellationToken);
    }
}
