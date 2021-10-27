using FluentValidation;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Infraestructure.Validators
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            _ = this.RuleFor(p => p.Pessoas.Id_Pessoas).NotEmpty().WithMessage("Id_Pessoa não pode ser vazio");
            _ = this.RuleFor(p => p.Pessoas.Nome).NotEmpty().WithMessage("Nome não pode ser vazio");
            _ = this.RuleFor(p => p.Pessoas.Sobrenome).NotEmpty().WithMessage("Sobrenome não pode ser vazio");
            _ = this.RuleFor(p => p.Pessoas.Pessoas_Calc_Number).NotEmpty().WithMessage("Numero não pode ser vazio");
            _ = this.RuleFor(p => p.Pessoas.DataHora).NotEmpty().WithMessage("DataHora não pode ser vazio");
        }
    }
}
