using MediatR;
using PessoasAPI.Context;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonCreateCommandHandler : IRequestHandler<PersonCreateCommand, Pessoas>
    {
        private readonly ContextDB _context;

        public PersonCreateCommandHandler(ContextDB context)
        {
            _context = context;
        }

        public async Task<Pessoas> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
        {
            var pessoas = new Pessoas();
            pessoas.Id_Pessoas = request.Id_Pessoas;
            pessoas.Nome = request.Nome;
            pessoas.Sobrenome = request.Sobrenome;
            pessoas.Pessoas_Calc_Number = request.Pessoas_Calc_Number;
            pessoas.DataHora = request.DataHora;
            _context.Pessoas.Add(pessoas);
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
