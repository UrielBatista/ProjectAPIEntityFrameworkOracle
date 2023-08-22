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
            _ = this.CreateMap<Persons, PersonsInfoDto>();
            _ = this.CreateMap<PersonsInfoDto, Persons>();

            _ = this.CreateMap<GraphQLPersonsInfoDto, Persons>();
            _ = this.CreateMap<PersonsInfoDto, GraphQLPersonsInfoDto>();

            _ = this.CreateMap<Persons, PersonsAllInfoDto>();
        }
    }
}
