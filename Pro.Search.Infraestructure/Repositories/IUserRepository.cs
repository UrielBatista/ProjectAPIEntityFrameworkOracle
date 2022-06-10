using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public interface IUserRepository
    {
        Task<UserEntity> FindUser(string username, string password, CancellationToken cancellationToken);
    }
}
