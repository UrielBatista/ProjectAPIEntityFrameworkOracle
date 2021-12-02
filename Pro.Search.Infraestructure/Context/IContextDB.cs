using Microsoft.EntityFrameworkCore;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Context
{
    public interface IContextDB
    {
        DbSet<Food> Food { get; set; }

        DbSet<Pessoas> Pessoas { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
