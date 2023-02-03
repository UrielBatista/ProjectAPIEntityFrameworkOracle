using FluentValidation;
using FluentValidation.Validators;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Linq;
using System.Threading;

namespace Pro.Search.Infraestructure.Validators.CustomerValidator
{
    internal sealed class CustomerPersonDtoValidator<T> : PropertyValidator<T, PersonDto>
    {
        private readonly IPersonsRepository personsRepository;

        public CustomerPersonDtoValidator(IPersonsRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }

        public override string Name => "ValidationCheckPersonAlreadyExist";

        public override bool IsValid(ValidationContext<T> context, PersonDto value)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = value ?? throw new ArgumentNullException(nameof(value));

            var dataAlreadyExist = personsRepository.FindAlreadyPersonWithEmailOrId(value.Pessoas.Email, value.Pessoas.Id_Pessoas, CancellationToken.None);

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
            => "Is not possible create person, data already exist in database";
    }
}
