using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public class PessoasRepository : IPessoasRepository
    {

        protected readonly ContextDB _context;

        private readonly DbSet<Pessoas> pessoas;

        public PessoasRepository(ContextDB context)
        {
            _context = context;
            this.pessoas = this._context.Pessoas;
        }

        public async Task<List<Pessoas>> FindAllAsyncPerson(CancellationToken cancellationToken)
        {
            return await _context.Pessoas.ToListAsync(cancellationToken);
        }

        public async Task<Pessoas> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);
        }
    }
}
