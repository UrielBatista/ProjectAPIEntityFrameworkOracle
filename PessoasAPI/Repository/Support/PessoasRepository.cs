using Microsoft.EntityFrameworkCore;
using PessoasAPI.Context;
using PessoasAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Repository
{
    public class PessoasRepository : IPessoasRepository
    {

        protected readonly ContextDB _context;

        public PessoasRepository(ContextDB context)
        {
            _context = context;
        }

        public async Task<string> CalcMediaAsync()
        {
            var results =  _context.Pessoas.OrderBy(x => x.Id_Pessoas).ToListAsync();
            var list = new List<float>();
            
            for (int i = 0; i < results.Result.Count; i++)
            {
                float receive_num = results.Result[i].Pessoas_Calc_Number;
                list.Add(receive_num);
            }

            double media = 0;
            double numSum = list.Sum();
            media = (numSum / results.Result.Count);
            var newNumMedia = media.ToString("F");
            return await Task.FromResult(newNumMedia);

        }
    }
}
