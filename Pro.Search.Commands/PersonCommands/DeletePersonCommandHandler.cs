using MediatR;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.DeleteResponses;

namespace Pro.Search.PersonCommands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, DeleteResponses>
    {
        private readonly ISystemDBContext _context;
        private readonly IPersonsRepository repository;

        public DeletePersonCommandHandler(ISystemDBContext _context, IPersonsRepository repository)
        {
            this._context = _context;
            this.repository = repository;
        }

        public async Task<DeleteResponses> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var pessoas = await this.repository.SearchAllPersonToIdPerson(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);

            if (!pessoas.Any()) 
                return new BadRequest($"Person with Id {request.Id_Pessoas} not exist in database, try delete person with other Id!");

            foreach(var item in pessoas)
            {
                _context.Pessoas.Remove(item);
            }
            await _context.SaveChangesAsync();
            return new Success(pessoas);
        }
    }
}
