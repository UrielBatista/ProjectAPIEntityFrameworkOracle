using MediatR;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class GetAllPersonQuery : IRequest<IEnumerable<Pessoas>>
    {
    }
}
