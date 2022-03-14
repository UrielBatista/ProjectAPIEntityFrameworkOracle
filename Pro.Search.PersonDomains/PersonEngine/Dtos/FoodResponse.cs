using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    public class FoodResponse
    {
        public List<FoodAllInfoDto> Foods { get; set; } = new List<FoodAllInfoDto>();

        public int Pages { get; set; }
        
        public int CurrentPage { get; set; }
    }
}
