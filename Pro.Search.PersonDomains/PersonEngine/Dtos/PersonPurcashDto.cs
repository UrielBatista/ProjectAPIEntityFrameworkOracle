using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    [JsonObject(ItemRequired = Required.AllowNull)]
    public class PersonPurcashDto
    {
        public PersonsAllInfoDto Pessoas { get; set; }

        public IEnumerable<FoodAllInfoDto> Food { get; set; }
    }
}
