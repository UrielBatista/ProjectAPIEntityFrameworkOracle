using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Domain.Commands.Responses
{
    public class CreatePessoasResponse
    {
        public Guid IdPessoas { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Date { get; set; }
    }
}
