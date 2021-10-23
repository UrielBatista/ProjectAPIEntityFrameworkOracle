using MediatR;
using Microsoft.EntityFrameworkCore;
using PessoasAPI.Context;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonDeleteCommandHandler : IRequestHandler<PersonDeleteCommand, Pessoas>
    {
        private readonly ContextDB _context;

        public PersonDeleteCommandHandler(ContextDB context)
        {
            _context = context;
        }

        public async Task<Pessoas> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            var pessoas = await _context.Pessoas.Where(a => a.Id_Pessoas == request.Id_Pessoas).FirstOrDefaultAsync();
            _context.Remove(pessoas);
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
