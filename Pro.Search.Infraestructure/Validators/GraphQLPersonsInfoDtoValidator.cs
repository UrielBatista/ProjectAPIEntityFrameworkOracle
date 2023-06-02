using FluentValidation;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Infraestructure.Validators
{
    public class GraphQLPersonsInfoDtoValidator : AbstractValidator<GraphQLPersonsInfoDto>
    {
        public GraphQLPersonsInfoDtoValidator()
        {
            _ = this.RuleFor(p => p.IdPessoas).NotEmpty().WithMessage("IdPessoas não pode ser vazio");
            _ = this.RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome não pode ser vazio");
            _ = this.RuleFor(p => p.Sobrenome).NotEmpty().WithMessage("Sobrenome não pode ser vazio");
            _ = this.RuleFor(p => p.Pessoas_Calc_Number).NotEmpty().WithMessage("Numero não pode ser vazio");
            _ = this.RuleFor(p => p.DataHora).NotEmpty().WithMessage("DataHora não pode ser vazio");
        }
    }
}
