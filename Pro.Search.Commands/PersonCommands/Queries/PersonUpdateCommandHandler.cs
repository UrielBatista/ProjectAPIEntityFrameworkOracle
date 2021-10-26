using MediatR;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonUpdateCommandHandler : IRequestHandler<PersonUpdateCommand, Pessoas>
    {
        private readonly ContextDB _context;

        public PersonUpdateCommandHandler(ContextDB context)
        {
            _context = context;
        }

        public async Task<Pessoas> Handle(PersonUpdateCommand request, CancellationToken cancellationToken)
        {
            var pessoas = _context.Pessoas.Where(a => a.Id_Pessoas == request.Id_Pessoas).FirstOrDefault();

            _ = pessoas ?? throw new ArgumentNullException(paramName: nameof(pessoas), message: "Argumento nao pode ser nulo");

            pessoas.Nome = request.Nome;
            pessoas.Sobrenome = request.Nome;
            pessoas.Pessoas_Calc_Number = request.Pessoas_Calc_Number;
            pessoas.DataHora = request.DataHora;
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
