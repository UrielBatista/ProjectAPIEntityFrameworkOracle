﻿using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly DbSet<Persons> pessoas;
        private readonly DbSet<Food> foods;

        public PersonsRepository(ISystemDBContext _context)
        {
            _ = _context ?? throw new ArgumentNullException(nameof(_context));
            this.pessoas = _context.Pessoas;
            this.foods = _context.Food;
        }

        public async Task<Persons> FindPersonPurcashFood(string Id_Pessoas, CancellationToken cancellationToken)
        {
            var personPurcashFood = await this.pessoas
                .Include(i => i.ComidaComprada.OrderBy(x => x.Id_Food))
                .FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);

            return personPurcashFood;
        }

        public async Task<List<Persons>> FindAllAsyncPerson(CancellationToken cancellationToken)
        {
            return await this.pessoas.ToListAsync(cancellationToken);
        }

        public async Task<Persons> FindOneAsyncPerson(string Id_Pessoas, CancellationToken cancellationToken)
        {
            return await this.pessoas.FirstOrDefaultAsync(p => p.Id_Pessoas == Id_Pessoas, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Persons>> SearchAllPersonToIdPerson(string id_pessoa, CancellationToken cancellationToken)
        {
            var pessoas = await this.pessoas
                .Include(i => i.ComidaComprada.Where(x => x.Id_Food != null))
                .Where(a => a.Id_Pessoas == id_pessoa)
                .ToListAsync();
            return pessoas;
        }

        public async Task<List<decimal>> CalcMediaPersonNumber(CancellationToken cancellationToken)
        {
            return await CreateTask(cancellationToken);

            async Task<List<decimal>> CreateTask(CancellationToken cancellationToken)
            {
                var allNumbersMediaPerson = await this.pessoas
                    .Select(p => p.Pessoas_Calc_Number)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                return allNumbersMediaPerson;
            }
        }

        public async Task<List<Persons>> FindAsyncPessoaWithFood(CancellationToken cancellationToken)
        {
            var pessoa = await this.pessoas
                .Join(this.foods, p => p.Id_Pessoas, f => f.Id_Pessoas_References, (pessoa, f) => new { pessoa, f })
                .Select(a => a.pessoa).Distinct()
                .OrderBy(a => a.Id_Pessoas)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return pessoa;
        }

        public async Task<IEnumerable<Persons>> FindListPersonsPurcashFoods(string[] Id_Pessoas, CancellationToken cancellationToken)
        {
            var personPurcashFood = await this.pessoas
                .Include(i => i.ComidaComprada.OrderBy(x => x.Id_Food))
                .Where(p => Id_Pessoas.Contains(p.Id_Pessoas))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return personPurcashFood;
        }

        public async Task<IEnumerable<Persons>> FindAlreadyPersonWithEmailOrId(string email, string Id_Pessoas, CancellationToken cancellationToken)
        {
            var personPurcashFood = await this.pessoas
                .Where(p => p.Email == email || p.Id_Pessoas == Id_Pessoas)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return personPurcashFood;
        }

        public async Task<List<Persons>> FindAllAsyncPersonWithId(string[] idPessoas, CancellationToken cancellationToken)
        {
            return await pessoas.Where(p => idPessoas.Contains(p.Id_Pessoas)).ToListAsync();
        }
    }
}
