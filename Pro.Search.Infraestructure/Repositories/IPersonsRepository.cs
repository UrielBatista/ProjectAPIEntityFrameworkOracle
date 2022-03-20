using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories
{
    public interface IPersonsRepository
    {
        Task<Persons> FindPersonPurcashFood(string Id_Pessoas, CancellationToken cancellationToken);

        Task<Persons> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken);
        
        Task<List<Persons>> FindAllAsyncPerson(CancellationToken cancellationToken);

        Task<List<Persons>> SearchAllPersonToIdPerson(string id_pessoa,CancellationToken cancellationToken);

        Task<List<Persons>> FindAsyncPessoaWithFood(CancellationToken cancellationToken);
    }
}
