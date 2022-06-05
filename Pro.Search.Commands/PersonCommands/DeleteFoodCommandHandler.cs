using MediatR;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, IEnumerable<Food>>
    {
        private readonly ISystemDBContext _context;
        private readonly IFoodRepository foodRepository;

        public DeleteFoodCommandHandler(ISystemDBContext _context, IFoodRepository foodRepository)
        {
            this._context = _context;
            this.foodRepository = foodRepository;
        }

        public async Task<IEnumerable<Food>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var foods = await foodRepository.FindListFoodReferenceToIDFood(request.Id_Food, cancellationToken);
            
            foreach (var item in foods)
            {
                _context.Food.Remove(item);
            }
            
            await _context.SaveChangesAsync();

            return foods;
        }
    }
}
