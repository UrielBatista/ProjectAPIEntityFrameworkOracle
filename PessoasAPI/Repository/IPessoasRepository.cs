using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Repository
{
    public interface IPessoasRepository
    {
        Task<string> CalcMediaAsync();
    }
}
