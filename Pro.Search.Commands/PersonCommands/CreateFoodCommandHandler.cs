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
    public class CreateFoodCommandHandler : ICommandHandler<CreateFoodCommand, FoodAllInfoDto>
    {
        private readonly ISystemDBContext _context;
        private readonly IFoodRepository repository;
        private readonly IMapper mapper;

        public CreateFoodCommandHandler(ISystemDBContext _context, IMapper mapper, IFoodRepository repository)
        {
            this._context = _context;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<FoodAllInfoDto> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var validityIdFood = await repository.FindOneAsyncFood(request.FoodAllInfoDto.Id_Food, cancellationToken).ConfigureAwait(false);

            // Persistence Entity in Database
            var returnValidation = new FoodAllInfoDto
            {
                Id_Food = request.FoodAllInfoDto.Id_Food,
                Nome = request.FoodAllInfoDto.Nome,
                LocalDeVenda = request.FoodAllInfoDto.LocalDeVenda,
                ReferenciaIdPessoa = request.FoodAllInfoDto.ReferenciaIdPessoa,
                PrecoComida = request.FoodAllInfoDto.PrecoComida
            };

            _ = await _context.Food.AddAsync(this.mapper.Map<FoodAllInfoDto, Food>(returnValidation), cancellationToken).ConfigureAwait(false);
            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return returnValidation;
        }
    }
}
