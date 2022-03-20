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
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, List<Persons>>
    {
        private readonly IContextDB _context;
        private readonly IPersonsRepository repository;

        public DeletePersonCommandHandler(IContextDB _context, IPersonsRepository repository)
        {
            this._context = _context;
            this.repository = repository;
        }

        public async Task<List<Persons>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var pessoas = await this.repository.SearchAllPersonToIdPerson(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);

            foreach(var item in pessoas)
            {
                _context.Pessoas.Remove(item);
            }
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
