using Pro.Search.PersonDomains.PersonEngine.Dtos;
namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class CreateOrUpdatePickerListResponses
    {
        public sealed class Success
        {
            public Success(PersonPickerDto persons)
            {
                this.Persons = persons;
            }

            public PersonPickerDto Persons { get; set; }
        }
    }
}
