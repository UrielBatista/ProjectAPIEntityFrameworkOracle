using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, PersonDto>
    {
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public CreatePersonCommandHandler(ContextDB context, IMapper mapper, IPessoasRepository repository)
        {
            _context = context;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var validationPerson = await CheckPersonExist(request, cancellationToken);

            CreatePersonCommandHandler.DefineLocalTime(request);

            if (validationPerson)
            {
                var returnValidation = new PersonDto
                {
                    Pessoas = request.PersonDto!.Pessoas,
                };

                _ = await _context.Pessoas.AddAsync(this.mapper.Map<PessoasInfoDto, Pessoas>(returnValidation.Pessoas), cancellationToken).ConfigureAwait(false);
                _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return returnValidation;
            }

            return null;
        }

        private async Task<bool> CheckPersonExist(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var personDb = await this.repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, cancellationToken).ConfigureAwait(false);
            if (personDb == null) return true;
            return false;

        }

        private static void DefineLocalTime(CreatePersonCommand request)
        {
            DateTime localDate = DateTime.Now;
            request.PersonDto.Pessoas.DataHora = localDate;
        }
    }
}
