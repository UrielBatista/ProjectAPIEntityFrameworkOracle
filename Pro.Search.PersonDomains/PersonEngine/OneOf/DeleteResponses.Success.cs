using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class DeleteResponses
    {
        public sealed class Success
        {
            public Success(List<Persons> persons)
            {
                this.Persons = persons;
            }

            public List<Persons> Persons { get; set; }
        }
    }
}
