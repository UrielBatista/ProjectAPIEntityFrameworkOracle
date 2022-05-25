using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class CreateOrUpdateResponses
    {
        public sealed class Success
        {
            public Success(PersonDto personDto)
            {
                this.PersonDto = personDto;
            }

            public PersonDto PersonDto { get; set; }
        }
    }
}
