using Microsoft.EntityFrameworkCore;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Context
{
    public interface ISystemWriteDBContext
    {
        DbSet<Food> Food { get; set; }

        DbSet<Persons> Pessoas { get; set; }

        DbSet<UserEntity> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
