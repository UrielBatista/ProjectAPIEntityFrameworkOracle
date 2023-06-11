using FluentValidation;
using FluentValidation.Validators;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Linq;
using System.Threading;

namespace Pro.Search.Infraestructure.Validators.CustomerValidator
{
    internal sealed class CustomerGraphQLPersonsInfoDtoValidator<T> : PropertyValidator<T, GraphQLPersonsInfoDto>
    {
        private readonly IPersonsRepository personsRepository;

        public CustomerGraphQLPersonsInfoDtoValidator(IPersonsRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }

        public override string Name => "ValidationCheckPersonAlreadyExist";

        public override bool IsValid(ValidationContext<T> context, GraphQLPersonsInfoDto value)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var dataAlreadyExist = personsRepository.FindAlreadyPersonWithEmailOrId(value.Email, value.IdPessoas, CancellationToken.None);

            if (!dataAlreadyExist.Result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Nao e possível criar essa pessoa. IdPessoa ja existe no banco";
    }
}
