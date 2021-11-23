using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class UpdateFoodCommandHandler : ICommandHandler<UpdateFoodCommand, FoodDto>
    {
        private readonly ContextDB _context;
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public UpdateFoodCommandHandler(ContextDB context, IFoodRepository repository, IMapper mapper)
        {
            _context = context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<FoodDto> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var foodDb = await this.repository.FindOneAsyncFood(request.FoodDto.Id_Food, cancellationToken);

            _ = foodDb ?? throw new ArgumentNullException(nameof(foodDb));

            foodDb.Id_Food = request.FoodDto.Id_Food;
            foodDb.Name_Food = request.FoodDto.Nome;
            foodDb.Locale_Purcache_Food = request.FoodDto.LocalDeVenda;
            foodDb.Id_Pessoas_References = request.FoodDto.ReferenciaIdPessoa;
            foodDb.Price_Food = request.FoodDto.PrecoComida;
            await _context.SaveChangesAsync();
            return request.FoodDto;
        }
    }
}
