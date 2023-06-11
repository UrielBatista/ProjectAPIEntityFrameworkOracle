using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    public class PersonWithFoodsDto
    {
        public PersonDto Person { get; set; }

        public IEnumerable<FoodDto> Foods { get; set; }
    }
}
