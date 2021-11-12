using FluentValidation;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Infraestructure.Validators
{
    public class FoodDtoValidator : AbstractValidator<FoodDto>
    {
        public FoodDtoValidator()
        {
            _ = this.RuleFor(p => p.Id_Food).NotEmpty().WithMessage("Id_Food não pode ser vazio");
            _ = this.RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome não pode ser vazio");
            _ = this.RuleFor(p => p.LocalDeVenda).NotEmpty().WithMessage("LocalDeVenda não pode ser vazio");
            _ = this.RuleFor(p => p.ReferenciaIdPessoa).NotEmpty().WithMessage("ReferenciaIdPessoa não pode ser vazio");
            _ = this.RuleFor(p => p.PrecoComida).NotEmpty().WithMessage("PrecoComida não pode ser vazio");
        }
    }
}
