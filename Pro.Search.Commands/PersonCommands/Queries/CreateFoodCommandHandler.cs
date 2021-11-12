using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class CreateFoodCommandHandler : ICommandHandler<CreateFoodCommand, FoodDto>
    {
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public CreateFoodCommandHandler(ContextDB context, IMapper mapper, IPessoasRepository repository)
        {
            _context = context;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<FoodDto> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var returnValidation = new FoodDto
            {
                Id_Food = request.FoodDto.Id_Food,
                Nome = request.FoodDto.Nome,
                LocalDeVenda = request.FoodDto.LocalDeVenda,
                ReferenciaIdPessoa = request.FoodDto.ReferenciaIdPessoa,
                PrecoComida = request.FoodDto.PrecoComida
            };

            _ = await _context.Food.AddAsync(this.mapper.Map<FoodDto, Food>(returnValidation), cancellationToken).ConfigureAwait(false);
            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return returnValidation;
        }
    }
}
