using MediatR;
using Microsoft.EntityFrameworkCore;
using PessoasAPI.Context;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, IEnumerable<Pessoas>>
    {
        private readonly ContextDB _context;

        public GetAllPersonQueryHandler(ContextDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoas>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pessoas.ToListAsync();
        }
    }
}
