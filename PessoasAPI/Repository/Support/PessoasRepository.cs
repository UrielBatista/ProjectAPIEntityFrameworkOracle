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

        public async Task<IList<Pessoas>> ListarPessoasAsync()
        {
            return await _context.Pessoas.OrderBy(x => x.Id_Pessoas).ToListAsync();
        }

        public async Task<Pessoas> ListarPessoaUnicaAsync(string id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public string CalcMediaAsync()
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
            return newNumMedia;

        }


        public async Task<Pessoas> InsertPessoasAsync(Pessoas pessoas)
        {

            Pessoas person = new Pessoas();
            person.Id_Pessoas = pessoas.Id_Pessoas;
            person.Nome = pessoas.Nome;
            person.Sobrenome = pessoas.Sobrenome;
            person.Pessoas_Calc_Number = pessoas.Pessoas_Calc_Number;
            person.DataHora = DateTime.Now;

            await _context.Pessoas.AddAsync(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Pessoas> AtualizarPessoasAsync(Pessoas pessoas)
        {
            Pessoas person = new Pessoas();
            person.Id_Pessoas = pessoas.Id_Pessoas;
            person.Nome = pessoas.Nome;
            person.Sobrenome = pessoas.Sobrenome;
            person.Pessoas_Calc_Number = pessoas.Pessoas_Calc_Number;
            person.DataHora = DateTime.Now;

            _context.Pessoas.Update(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public string DeletePessoasAsync(Pessoas pessoas)
        {
            _context.Pessoas.Remove(pessoas);
            _context.SaveChanges();
            return pessoas.Nome.ToString();
        }
    }
}
