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
    public class FoodRepository : IFoodRepository
    {

        private readonly DbSet<Food> foods;

        public FoodRepository(IContextDB _context)
        {
            _ = _context ?? throw new ArgumentNullException(nameof(_context));
            this.foods = _context.Food;
        }

        public async Task<Food> FindOneAsyncFood(string Id_Food, CancellationToken cancellationToken)
        {
            return await this.foods.FirstOrDefaultAsync(f => f.Id_Food == Id_Food, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Food>> FindAllAsyncFood(int page, int pageSize, CancellationToken cancellationToken)
        {
            var foods =  await this.foods.ToListAsync(cancellationToken);

            var pagePickup = foods.Skip((page - 1) * (int)pageSize).Take((int)pageSize).OrderBy(foods => foods.Id_Food).ToList();

            return pagePickup;
        }

        public async Task<IEnumerable<Food>> FindAllAsyncFoodReferenceToPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            var foodReferencedPerson = await this.foods.Where(p =>  p.Id_Pessoas_References == Id_Pessoas).ToListAsync().ConfigureAwait(false);

            return foodReferencedPerson;
        }

        public async Task<Food> FindOneAsyncFoodReferenceToPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            var foodReferencedPersonOne = await this.foods.FirstOrDefaultAsync(p => p.Id_Pessoas_References == Id_Pessoas, cancellationToken).ConfigureAwait(false);

            return foodReferencedPersonOne;
        }

        public async Task<List<Food>> FindListFoodReferenceToIDFood(string id_food, CancellationToken cancellationToken)
        {
            var result = await this.foods.Where(a => a.Id_Food == id_food).ToListAsync();
            return result;
        }
    }
}
