using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonCreateCommandHandler : ICommandHandler<PersonCreateCommand, PersonDto>
    {
        private readonly ContextDB _context;
        private readonly IMapper mapper;

        public PersonCreateCommandHandler(ContextDB context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<PersonDto> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var returnValidation = new PersonDto
            {
                Pessoas = request.PersonDto!.Pessoas,
            };

            _ = await _context.Pessoas.AddAsync(this.mapper.Map<PessoasInfoDto, Pessoas>(returnValidation.Pessoas), cancellationToken).ConfigureAwait(false);
            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return returnValidation;
        }
    }
}
