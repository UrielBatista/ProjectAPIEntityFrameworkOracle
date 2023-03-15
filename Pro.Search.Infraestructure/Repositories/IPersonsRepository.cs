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

        Task<List<Persons>> FindAllAsyncPersonWithId(string[] idPessoas, CancellationToken cancellationToken);

        Task<List<decimal>> CalcMediaPersonNumber(CancellationToken cancellationToken);

        Task<List<Persons>> SearchAllPersonToIdPerson(string id_pessoa,CancellationToken cancellationToken);

        Task<List<Persons>> FindAsyncPessoaWithFood(CancellationToken cancellationToken);

        Task<IEnumerable<Persons>> FindListPersonsPurcashFoods(string[] Id_Pessoas, CancellationToken cancellationToken);

        Task<IEnumerable<Persons>> FindAlreadyPersonWithEmailOrId(string email, string Id_Pessoas, CancellationToken cancellationToken);
    }
}
