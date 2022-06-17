using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class SetPessoasPurcashCommandHandler : ICommandHandler<SetPessoasPurcashCommand, PersonPurcashDto>
    {
        private readonly Random _random = new Random();
        private readonly ISystemDBContext _context;
        private readonly IPersonsRepository repository;
        private readonly IFoodRepository repositoryFood;
        private readonly IMapper mapper;

        public SetPessoasPurcashCommandHandler(ISystemDBContext _context, IFoodRepository repositoryFood, IPersonsRepository repository, IMapper mapper)
        {
            this._context = _context;
            this.repository = repository;
            this.repositoryFood = repositoryFood;
            this.mapper = mapper;
        }

        public async Task<PersonPurcashDto> Handle(SetPessoasPurcashCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var dataPerson = new PersonPurcashDto 
            { 
            };

            dataPerson.Pessoas = await this.UpdatePessoasAsync(request.Id_Pessoa, request.PersonPurcashDto.Pessoas, cancellationToken).ConfigureAwait(false);

            dataPerson.Food = await this.UpdateFoodAsync(request.Id_Pessoa, request.PersonPurcashDto.Food.ToList(), cancellationToken).ConfigureAwait(false);

            return dataPerson;
        }

        private async Task<PersonsAllInfoDto> UpdatePessoasAsync(string id_pessoa, PersonsAllInfoDto pessoasAllInfoDto, CancellationToken cancellationToken)
        {
            _ = id_pessoa ?? throw new ArgumentException(nameof(id_pessoa)); 
            _ = pessoasAllInfoDto ?? throw new ArgumentException(nameof(pessoasAllInfoDto));

            var personDb = await this.repository.FindOneAsyncPerson(id_pessoa, cancellationToken).ConfigureAwait(false);
            
            personDb.Nome = pessoasAllInfoDto.Nome != null ? pessoasAllInfoDto.Nome : personDb.Nome;
            personDb.Sobrenome = pessoasAllInfoDto.Sobrenome != null ? pessoasAllInfoDto.Sobrenome : personDb.Sobrenome;
            personDb.Email = pessoasAllInfoDto.Email != null ? pessoasAllInfoDto.Email : personDb.Email;
            personDb.Pessoas_Calc_Number = pessoasAllInfoDto.Pessoas_Calc_Number != 0 ? pessoasAllInfoDto.Pessoas_Calc_Number : personDb.Pessoas_Calc_Number;
            personDb.DataHora = DateTime.Now.ToString() != null ? DateTime.Now : personDb.DataHora;
            await _context.SaveChangesAsync();
            return pessoasAllInfoDto;
        }

        private async Task<IEnumerable<FoodAllInfoDto>> UpdateFoodAsync(string id_pessoa, List<FoodAllInfoDto> foodDto, CancellationToken cancellationToken)
        {
            _ = foodDto ?? throw new ArgumentException(nameof(foodDto));
            
            if (foodDto.Count == 1)
            {
                foreach(var data in foodDto)
                {
                    var food = await this.repositoryFood.FindOneAsyncFoodReferenceToPerson(id_pessoa, cancellationToken).ConfigureAwait(false);

                    if (food == null)
                    {
                        var returnValidation = new FoodAllInfoDto
                        {
                            Id_Food = data.Id_Food != null ? data.Id_Food : food.Id_Food,
                            Nome = data.Nome != null ? data.Nome : food.Name_Food,
                            LocalDeVenda = data.LocalDeVenda != null ? data.LocalDeVenda : food.Locale_Purcache_Food,
                            ReferenciaIdPessoa = data.ReferenciaIdPessoa != null ? data.ReferenciaIdPessoa : food.Id_Pessoas_References,
                            PrecoComida = data.PrecoComida != 0 ? data.PrecoComida : food.Price_Food
                        };

                        _ = await _context.Food.AddAsync(this.mapper.Map<FoodAllInfoDto, Food>(returnValidation), cancellationToken).ConfigureAwait(false);
                        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return foodDto;
                    }

                    food.Id_Food = data.Id_Food != null ? data.Id_Food : food.Id_Food;
                    food.Name_Food = data.Nome != null ? data.Nome : food.Name_Food;
                    food.Locale_Purcache_Food = data.LocalDeVenda != null ? data.LocalDeVenda : food.Locale_Purcache_Food;
                    food.Id_Pessoas_References = data.ReferenciaIdPessoa != null ? data.ReferenciaIdPessoa : food.Id_Pessoas_References;
                    food.Price_Food = data.PrecoComida != 0 ? data.PrecoComida : food.Price_Food;
                    await _context.SaveChangesAsync();
                    return foodDto;
                }
            }

            var foodDb = await this.repositoryFood.FindAllAsyncFoodReferenceToPerson(id_pessoa, cancellationToken);
            int count = 0;
            foreach (var data in foodDto)
            {
                if (count > 0)
                {
                    if (count >= 2)
                    {
                        foodDb = foodDb.Skip(count - 1);
                    }
                    else
                    {
                        foodDb = foodDb.Skip(count);
                    }
                }

                foreach (var newDate in foodDb) 
                {
                    newDate.Id_Food = data.Id_Food != null ? data.Id_Food : newDate.Id_Food;
                    newDate.Name_Food = data.Nome != null ? data.Nome : newDate.Name_Food;
                    newDate.Locale_Purcache_Food = data.LocalDeVenda != null ? data.LocalDeVenda : newDate.Locale_Purcache_Food;
                    newDate.Id_Pessoas_References = data.ReferenciaIdPessoa != null ? data.ReferenciaIdPessoa : newDate.Id_Pessoas_References;
                    newDate.Price_Food = data.PrecoComida != 0 ? data.PrecoComida : newDate.Price_Food;
                    await _context.SaveChangesAsync();
                    count += 1;
                    break;
                }
            }

            if (foodDto.Count() > foodDb.Count())
            {
                var result = foodDto.Skip(count);

                foreach (var newDate in result)
                {
                    var food = await this.repositoryFood.FindOneAsyncFood(newDate.Id_Food, cancellationToken).ConfigureAwait(false);
                    if (food == null)
                    {
                        var returnValidation = new FoodAllInfoDto
                        {
                            Id_Food = newDate.Id_Food != null ? newDate.Id_Food : food.Id_Food,
                            Nome = newDate.Nome != null ? newDate.Nome : food.Name_Food,
                            LocalDeVenda = newDate.LocalDeVenda != null ? newDate.LocalDeVenda : food.Locale_Purcache_Food,
                            ReferenciaIdPessoa = newDate.ReferenciaIdPessoa != null ? newDate.ReferenciaIdPessoa : food.Id_Pessoas_References,
                            PrecoComida = newDate.PrecoComida != 0 ? newDate.PrecoComida : food.Price_Food
                        };

                        _ = await _context.Food.AddAsync(this.mapper.Map<FoodAllInfoDto, Food>(returnValidation), cancellationToken).ConfigureAwait(false);
                        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }
                }
            }

            return foodDto;
        }

        private string RandomNumber(int min, int max)
        {
            return _random.Next(min, max).ToString();
        }
    }
}
