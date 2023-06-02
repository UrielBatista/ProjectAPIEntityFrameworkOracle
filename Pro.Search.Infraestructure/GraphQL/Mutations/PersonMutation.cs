using AppAny.HotChocolate.FluentValidation;
using AutoMapper;
using HotChocolate;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Validators;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.GraphQL.Mutations
{
    public class PersonMutation
    {
        private readonly IMapper mapper;

        public PersonMutation(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<GraphQLPersonsInfoDto> UpdateOrCreatePersons(
            [UseFluentValidation, UseValidator<GraphQLPersonsInfoDtoValidator>] GraphQLPersonsInfoDto persons,
            [Service] ISystemDBContext context,
            [Service] ITopicEventSender sender,
            CancellationToken cancellationToken)
        {
            var validationPerson = await context.Pessoas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id_Pessoas == persons.IdPessoas, cancellationToken)
                .ConfigureAwait(false);

            if (validationPerson == null)
            {
                DefineLocalTime(persons);
                var personDB = this.mapper.Map<GraphQLPersonsInfoDto, Persons>(persons);
                personDB.Id_Pessoas = persons.IdPessoas;
                await context.Pessoas.AddAsync(personDB, cancellationToken).ConfigureAwait(false);
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await sender.SendAsync("CreatedGraphQLPerson", persons).ConfigureAwait(false);
            } 
            else
            {
                try
                {
                    var personUpdateDB = this.mapper.Map<GraphQLPersonsInfoDto, Persons>(persons);
                    personUpdateDB.Id_Pessoas = persons.IdPessoas;
                    _ = context.Pessoas.Attach(personUpdateDB);
                    _ = context.Pessoas.Entry(personUpdateDB).State = EntityState.Modified;
                    _ = await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    await sender.SendAsync("UpdatedGraphQLPerson", persons).ConfigureAwait(false);
                }
                catch(Exception ex)
                {
                    _ = ex;
                    throw new GraphQLException(
                        ErrorBuilder
                        .New()
                        .SetMessage("Error in update person", ex)
                        .Build());
                }
                
            }

            return persons;
        }

        private static void DefineLocalTime(GraphQLPersonsInfoDto request)
        {
            DateTime localDate = DateTime.Now;
            request.DataHora = localDate;
        }
    }
}
