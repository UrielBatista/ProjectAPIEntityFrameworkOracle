using MediatR;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PessoasAPI.Domain.Commands.Requests;
using PessoasAPI.Domain.Commands.Responses;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace PessoasAPI.Domain.Commands.Handlers
{
    public class CreatePessoasHandler : IRequestHandler<CreatePessoasRequest, CreatePessoasResponse>
    {
        private readonly string _connectionString;

        public CreatePessoasHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
        }
        public async Task<CreatePessoasResponse> Handle(CreatePessoasRequest request, CancellationToken cancellationToken)
        {
                try
                {
                    var result = new CreatePessoasResponse
                    {
                        IdPessoas = Guid.NewGuid(),
                        Nome = "Uriel",
                        Sobrenome = "Batista",
                        Date = DateTime.Now
                    };
                    return await Task.FromResult(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error", e);
                    throw e;
                }
            }
        }
    }
