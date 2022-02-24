using System.Collections.Generic;
using System.Text.Json;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class ErrorDetails
    {
        public string Type { get; set; }

        public int StatusCode { get; set; }

        public string MessageTitle { get; set; }

        public IEnumerable<IDictionary<string, string>> Errors { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
