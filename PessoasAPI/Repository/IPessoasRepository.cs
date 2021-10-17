using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Repository
{
    public interface IPessoasRepository
    {
        Task<IList<Pessoas>> ListarPessoasAsync();

        Task<Pessoas> ListarPessoaUnicaAsync(string id);

        Task<Pessoas> InsertPessoasAsync(Pessoas pessoas);

        Task<Pessoas> AtualizarPessoasAsync(Pessoas pessoas);

        string CalcMediaAsync();

        string DeletePessoasAsync(Pessoas pessoas);

        
    }
}
