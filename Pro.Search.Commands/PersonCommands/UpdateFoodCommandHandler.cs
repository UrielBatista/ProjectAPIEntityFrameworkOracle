using AutoMapper;
using MediatR;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, FoodAllInfoDto>
    {
        private readonly ISystemDBContext _context;
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public UpdateFoodCommandHandler(ISystemDBContext _context, IFoodRepository repository, IMapper mapper)
        {
            this._context = _context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<FoodAllInfoDto> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var foodDb = await this.repository.FindOneAsyncFood(request.FoodAllInfoDto.Id_Food, cancellationToken);

            _ = foodDb ?? throw new ArgumentNullException(nameof(foodDb));

            foodDb.Id_Food = request.FoodAllInfoDto.Id_Food;
            foodDb.Name_Food = request.FoodAllInfoDto.Nome;
            foodDb.Locale_Purcache_Food = request.FoodAllInfoDto.LocalDeVenda;
            foodDb.Id_Pessoas_References = request.FoodAllInfoDto.ReferenciaIdPessoa;
            foodDb.Price_Food = request.FoodAllInfoDto.PrecoComida;
            await _context.SaveChangesAsync();
            return request.FoodAllInfoDto;
        }
    }
}
