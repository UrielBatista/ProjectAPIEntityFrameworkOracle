using MediatR;
using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, List<Food>>
    {
        private readonly IContextDB _context;

        public DeleteFoodCommandHandler(IContextDB _context)
        {
            this._context = _context;
        }

        public async Task<List<Food>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var foods = await _context.Food.Where(a => a.Id_Food == request.Id_Food).ToListAsync();
            foreach (var item in foods)
            {
                _context.Food.Remove(item);
            }
            await _context.SaveChangesAsync();
            return foods;
        }
    }
}
