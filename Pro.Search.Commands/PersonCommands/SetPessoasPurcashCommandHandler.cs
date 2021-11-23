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
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;
        private readonly IFoodRepository repositoryFood;
        private readonly IMapper mapper;

        public SetPessoasPurcashCommandHandler(ContextDB context, IFoodRepository repositoryFood, IPessoasRepository repository, IMapper mapper)
        {
            _context = context;
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

            dataPerson.Food = await this.UpdateFoodAsync(request.PersonPurcashDto.Food.ToList(), cancellationToken).ConfigureAwait(false);


            

            return dataPerson;
        }

        private async Task<PessoasAllInfoDto> UpdatePessoasAsync(string id_pessoa, PessoasAllInfoDto pessoasAllInfoDto, CancellationToken cancellationToken)
        {
            _ = id_pessoa ?? throw new ArgumentException(nameof(id_pessoa)); 
            _ = pessoasAllInfoDto ?? throw new ArgumentException(nameof(pessoasAllInfoDto));

            var personDb = await this.repository.FindOneAsyncPerson(id_pessoa, cancellationToken).ConfigureAwait(false);
            //var dataRetornoPessoasAllInfoDto = new Pessoas
            //{
            //    Id_Pessoas = pessoasAllInfoDto.Id_Pessoas,
            //    Nome = pessoasAllInfoDto.Nome,
            //    Sobrenome = pessoasAllInfoDto.Sobrenome,
            //    Pessoas_Calc_Number = pessoasAllInfoDto.Pessoas_Calc_Number,
            //    DataHora = DateTime.Now.Date,
            //    ComidaComprada = null
            //};

            //_context.Pessoas.Update(dataRetornoPessoasAllInfoDto);
            personDb.Nome = pessoasAllInfoDto.Nome;
            personDb.Sobrenome = pessoasAllInfoDto.Sobrenome;
            personDb.Pessoas_Calc_Number = pessoasAllInfoDto.Pessoas_Calc_Number;
            personDb.DataHora = DateTime.Now;
            await _context.SaveChangesAsync();
            return pessoasAllInfoDto;
        }

        private async Task<IEnumerable<FoodDto>> UpdateFoodAsync(List<FoodDto> foodDto, CancellationToken cancellationToken)
        {
            _ = foodDto ?? throw new ArgumentException(nameof(foodDto));
            
            if (foodDto.Count == 1)
            {
                foreach(var data in foodDto)
                {
                    var foodDb = await this.repositoryFood.FindOneAsyncFood(data.Id_Food, cancellationToken).ConfigureAwait(false);

                    if (foodDb == null)
                    {
                        var returnValidation = new FoodDto
                        {
                            Id_Food = data.Id_Food,
                            Nome = data.Nome,
                            LocalDeVenda = data.LocalDeVenda,
                            ReferenciaIdPessoa = data.ReferenciaIdPessoa,
                            PrecoComida = data.PrecoComida
                        };

                        _ = await _context.Food.AddAsync(this.mapper.Map<FoodDto, Food>(returnValidation), cancellationToken).ConfigureAwait(false);
                        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        return foodDto;
                    }

                    foodDb.Id_Food = data.Id_Food;
                    foodDb.Name_Food = data.Nome;
                    foodDb.Locale_Purcache_Food = data.LocalDeVenda;
                    foodDb.Id_Pessoas_References = data.ReferenciaIdPessoa;
                    foodDb.Price_Food = data.PrecoComida;
                    await _context.SaveChangesAsync();
                    return foodDto;
                }
            }

            foreach(var dataFood in foodDto)
            {
                var dataRetornoFoodDto = new Food
                {
                    Id_Food = dataFood.Id_Food,
                    Name_Food = dataFood.Nome,
                    Locale_Purcache_Food = dataFood.LocalDeVenda,
                    Id_Pessoas_References = dataFood.ReferenciaIdPessoa,
                    Price_Food = dataFood.PrecoComida
                };

                _context.Food.Update(dataRetornoFoodDto);
            }

            await _context.SaveChangesAsync();

            return foodDto;
        }
    }
}
