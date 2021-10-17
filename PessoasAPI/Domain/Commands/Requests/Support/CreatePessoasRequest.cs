using MediatR;
using PessoasAPI.Domain.Commands.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Domain.Commands.Requests
{
    public class CreatePessoasRequest : IRequest<CreatePessoasResponse>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
