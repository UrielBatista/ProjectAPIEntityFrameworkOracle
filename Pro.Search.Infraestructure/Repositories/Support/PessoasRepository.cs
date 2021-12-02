using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public class PessoasRepository : IPessoasRepository
    {
        private readonly DbSet<Pessoas> pessoas;

        public PessoasRepository(IContextDB _context)
        {
            _ = _context ?? throw new ArgumentNullException(nameof(_context));
            this.pessoas = _context.Pessoas;
        }

        public async Task<Pessoas> FindPersonPurcashFood(string Id_Pessoas, CancellationToken cancellationToken)
        {
            var personPurcashFood = await this.pessoas
                .Include(i => i.ComidaComprada)
                .FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);

            return personPurcashFood;
        }

        public async Task<List<Pessoas>> FindAllAsyncPerson(CancellationToken cancellationToken)
        {
            return await this.pessoas.ToListAsync(cancellationToken);
        }

        public async Task<Pessoas> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            return await this.pessoas.FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);
        }

        public Pessoas DeletePersonToIdPessoa(Pessoas pessoas, CancellationToken cancellationToken)
        {
            _ = this.pessoas.Remove(pessoas);
            return pessoas;
        }

        public async Task<List<Pessoas>> SearchAllPersonToIdPerson(string id_pessoa, CancellationToken cancellationToken)
        {
            var pessoas = await this.pessoas.Where(a => a.Id_Pessoas == id_pessoa).ToListAsync();
            return pessoas;
        }
    }
}
