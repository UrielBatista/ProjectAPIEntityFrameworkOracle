using FluentValidation;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.Infraestructure.Validators.CustomerValidator;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Validators.Extensions
{
    internal static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, PersonDto> PersonCheckDataAlreadyExist<T>(
            this IRuleBuilder<T, PersonDto> ruleBuilder,
            IPersonsRepository personsRepository)
        {
            _ = ruleBuilder ?? throw new ArgumentNullException(nameof(ruleBuilder));

            return ruleBuilder.SetValidator(new CustomerPersonDtoValidator<T>(personsRepository));
        }
    }
}
