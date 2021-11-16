using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine;
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

        private readonly DbSet<Food> foods;

        public PessoasRepository(ContextDB context)
        {
            _context = context;
            this.pessoas = this._context.Pessoas;
            this.foods = this._context.Food;
        }

        public async Task<List<Pessoas>> FindAllAsyncPerson(CancellationToken cancellationToken)
        {
            return await _context.Pessoas.ToListAsync(cancellationToken);
        }

        public async Task<Pessoas> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Food>> FindAllAsyncFood(CancellationToken cancellationToken)
        {
            return await _context.Food.ToListAsync(cancellationToken);
        }

        public async Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken)
        {
            return await _context.Food.FirstOrDefaultAsync(f => f.Id_Food == Id_Food, cancellationToken).ConfigureAwait(false);
        }
    }
}
