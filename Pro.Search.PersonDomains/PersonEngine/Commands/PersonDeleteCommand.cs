using MediatR;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonDeleteCommand : IRequest<Pessoas>
    {
        public string Id_Pessoas { get; set; }
    }
}
