using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using ServiceStack.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class CreateFoodInMemoryCommandHandler : ICommandHandler<CreateFoodInMemoryCommand, FoodAllInfoDto>
    {
        private readonly IContextDB _context;
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public CreateFoodInMemoryCommandHandler(IContextDB _context, IMapper mapper, IFoodRepository repository)
        {
            this._context = _context;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<FoodAllInfoDto> Handle(CreateFoodInMemoryCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var validityIdFood = await repository.FindOneAsyncFood(request.FoodAllInfoDto.Id_Food, cancellationToken).ConfigureAwait(false);

            // Create Entity in Memory
            var dataRedis = new FoodInMemory
            {
                Name_Food = request.FoodAllInfoDto.Nome,
                Locale_Purcache_Food = request.FoodAllInfoDto.LocalDeVenda,
                Id_Pessoas_References = request.FoodAllInfoDto.ReferenciaIdPessoa,
                Price_Food = request.FoodAllInfoDto.PrecoComida
            };


            var host = "localhost:6379";
            using (var redisclient = new RedisClient(host))
            {
                redisclient.Set<FoodInMemory>(dataRedis.Id_Food.ToString(), dataRedis);
            }

            var returnValidation = new FoodAllInfoDto
            {
                Id_Food = request.FoodAllInfoDto.Id_Food,
                Nome = request.FoodAllInfoDto.Nome,
                LocalDeVenda = request.FoodAllInfoDto.LocalDeVenda,
                ReferenciaIdPessoa = request.FoodAllInfoDto.ReferenciaIdPessoa,
                PrecoComida = request.FoodAllInfoDto.PrecoComida
            };

            return returnValidation;
        }
    }
}
