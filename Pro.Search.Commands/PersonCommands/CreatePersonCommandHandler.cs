using AutoMapper;
using MassTransit;
using MediatR;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdateResponses;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreateOrUpdateResponses>
    {
        private readonly ISystemWriteDBContext _context;
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public CreatePersonCommandHandler(
            ISystemWriteDBContext _context,
            IMapper mapper, 
            IPersonsRepository repository)
        {
            this._context = _context;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<CreateOrUpdateResponses> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var validationPerson = await CheckPersonExist(request, cancellationToken);

            CreatePersonCommandHandler.DefineLocalTime(request);

            if (string.IsNullOrEmpty(validationPerson.Id_Pessoas) || !validationPerson.Id_Pessoas.Any())
            {
                var returnValidation = new PersonDto
                {
                    Pessoas = request.PersonDto!.Pessoas,
                };

                _ = await _context.Pessoas.AddAsync(this.mapper.Map<PersonsInfoDto, Persons>(returnValidation.Pessoas), cancellationToken).ConfigureAwait(false);
                _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return new Success(returnValidation);
            }

            return new BadRequest("An error occurred while saving the data!");
        }

        private async Task<Persons> CheckPersonExist(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var personDb = await this.repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, cancellationToken).ConfigureAwait(false);
            if (personDb == null) return new Persons();
            return personDb;
        }

        private static void DefineLocalTime(CreatePersonCommand request)
        {
            DateTime localDate = DateTime.Now;
            request.PersonDto.Pessoas.DataHora = localDate;
        }
    }
}
