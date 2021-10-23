using MediatR;
using Microsoft.EntityFrameworkCore;
using PessoasAPI.Context;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Queries
{
    public class GetOnePersonQueryHandler : IRequestHandler<GetOnePersonQuery, Pessoas>
    {
        private readonly ContextDB _context;

        public GetOnePersonQueryHandler(ContextDB context)
        {
            _context = context;
        }

        public async Task<Pessoas> Handle(GetOnePersonQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(x => x.Id_Pessoas == request.Id_Pessoas, cancellationToken);
        }
    }
}
