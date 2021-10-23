using MediatR;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonUpdateCommand : IRequest<Pessoas>
    {
        public string Id_Pessoas { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public float Pessoas_Calc_Number { get; set; }

        public DateTime DataHora { get; set; }
    }
}
