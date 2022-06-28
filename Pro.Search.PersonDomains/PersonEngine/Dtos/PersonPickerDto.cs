using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    public class PersonPickerDto
    {
        public IEnumerable<PersonPickerNewListDto> Values { get; set; }

        public IEnumerable<PersonPickerNewListDto> Removeds { get; set; }
    }
}
