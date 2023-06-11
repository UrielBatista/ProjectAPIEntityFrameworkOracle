using FluentValidation;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Validators
{
    public class PersonWithFoodsDtoValidator : AbstractValidator<PersonWithFoodsDto>
    {
        public PersonWithFoodsDtoValidator(IPersonsRepository personsRepository) 
        {
            this.RuleSet("GraphQLPersonWithFoods", () =>
            {
                _ = this.RuleFor(p => p.Person).SetValidator(new PersonDtoValidator(personsRepository), ruleSets: "GraphQLPerson");
                _ = this.RuleForEach(p => p.Foods).SetValidator(new FoodDtoValidator(), ruleSets: "GraphQLFoods");
            });
        }
    }
}
