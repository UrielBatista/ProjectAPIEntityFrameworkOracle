using AutoMapper;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.Infraestructure.Mappers
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            _ = this.CreateMap<Food, FoodDto>()
                .ForMember(dist => dist.Id_Food, src => src.MapFrom(src => src.Id_Food))
                .ForMember(dist => dist.Nome, src => src.MapFrom(src => src.Name_Food))
                .ForMember(dist => dist.LocalDeVenda, src => src.MapFrom(src => src.Locale_Purcache_Food))
                .ForMember(dist => dist.ReferenciaIdPessoa, src => src.MapFrom(src => src.Id_Pessoas_References))
                .ForMember(dist => dist.PrecoComida, src => src.MapFrom(src => src.Price_Food));

            _ = this.CreateMap<FoodDto, Food>()
                .ForMember(dist => dist.Id_Food, src => src.MapFrom(src => src.Id_Food))
                .ForMember(dist => dist.Name_Food, src => src.MapFrom(src => src.Nome))
                .ForMember(dist => dist.Locale_Purcache_Food, src => src.MapFrom(src => src.LocalDeVenda))
                .ForMember(dist => dist.Id_Pessoas_References, src => src.MapFrom(src => src.ReferenciaIdPessoa))
                .ForMember(dist => dist.Price_Food, src => src.MapFrom(src => src.PrecoComida));
        }
    }
}
