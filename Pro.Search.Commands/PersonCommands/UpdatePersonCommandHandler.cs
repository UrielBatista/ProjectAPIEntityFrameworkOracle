using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdateResponses;

namespace Pro.Search.PersonCommands
{
    public class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, CreateOrUpdateResponses>
    {
        private readonly ISystemDBContext _context;
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;

        public UpdatePersonCommandHandler(ISystemDBContext _context, IPersonsRepository repository, IMapper mapper)
        {
            this._context = _context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CreateOrUpdateResponses> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var personDb = await this.repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, cancellationToken).ConfigureAwait(false);

            if (personDb == null) return new NotFound($"Person with Id {request.PersonDto.Pessoas.Id_Pessoas} not exist in database, pleaze create person first with Id");

            var dateTime = UpdatePersonCommandHandler.DefineLocalTime(request);
            personDb.Nome = request.PersonDto.Pessoas.Nome;
            personDb.Sobrenome = request.PersonDto.Pessoas.Sobrenome;
            personDb.Pessoas_Calc_Number = request.PersonDto.Pessoas.Pessoas_Calc_Number;
            personDb.DataHora = dateTime;
            await _context.SaveChangesAsync();
            return new Success(request.PersonDto);
        }

        private static DateTime DefineLocalTime(UpdatePersonCommand request)
        {
            DateTime localDate = DateTime.Now;
            request.PersonDto.Pessoas.DataHora = localDate;
            return localDate;
        }
    }
}
