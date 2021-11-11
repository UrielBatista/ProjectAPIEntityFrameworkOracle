using AutoMapper;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Pro.Search.Infraestructure.Mappers
{
    public class PersonProfile : Profile
    {
        [ExcludeFromCodeCoverage]
        public PersonProfile()
        {
            _ = this.CreateMap<Pessoas, PessoasInfoDto>();
            _ = this.CreateMap<PessoasInfoDto, Pessoas>();

            _ = this.CreateMap<Pessoas, PessoasAllInfoDto>();
            
            
        }
    }
}
