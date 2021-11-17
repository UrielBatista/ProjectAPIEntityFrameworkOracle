using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public class FoodRepository : IFoodRepository
    {
        protected readonly ContextDB _context;

        private readonly DbSet<Food> foods;

        public FoodRepository(ContextDB context)
        {
            _context = context;
            this.foods = this._context.Food;
        }

        public async Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken)
        {
            return await _context.Food.FirstOrDefaultAsync(f => f.Id_Food == Id_Food, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Food>> FindAllAsyncFood(CancellationToken cancellationToken)
        {
            return await _context.Food.ToListAsync(cancellationToken);
        }
    }
}
