using AutoMapper;
using BuldBlocks.Domain.Commons;
using MassTransit;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.PersonDomains.PersonEngine.Events;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdateResponses;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, CreateOrUpdateResponses>
    {
        private readonly ISystemDBContext _context;
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publish;

        public CreatePersonCommandHandler(
            ISystemDBContext _context,
            IMapper mapper, 
            IPersonsRepository repository, 
            IPublishEndpoint publish)
        {
            this._context = _context;
            this.mapper = mapper;
            this.repository = repository;
            this.publish = publish;
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

                await this.publish.Publish<PersonCreatedEvent>(new
                {
                    Id_Pessoas = request.PersonDto.Pessoas.Id_Pessoas,
                    Nome = request.PersonDto.Pessoas.Nome,
                    Sobrenome = request.PersonDto.Pessoas.Sobrenome,
                    Email = request.PersonDto.Pessoas.Email,
                    Pessoas_Calc_Number = request.PersonDto.Pessoas.Pessoas_Calc_Number,
                    DataHora = request.PersonDto.Pessoas.DataHora,
                });

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
