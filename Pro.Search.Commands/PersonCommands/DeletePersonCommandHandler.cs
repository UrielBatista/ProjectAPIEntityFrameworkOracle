using MediatR;
using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, List<Pessoas>>
    {
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;

        public DeletePersonCommandHandler(ContextDB context, IPessoasRepository repository)
        {
            _context = context;
            this.repository = repository;
        }

        public async Task<List<Pessoas>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var pessoas = await _context.Pessoas.Where(a => a.Id_Pessoas == request.Id_Pessoas).ToListAsync();
            foreach(var item in pessoas)
            {
                _context.Remove(item);
            }
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
