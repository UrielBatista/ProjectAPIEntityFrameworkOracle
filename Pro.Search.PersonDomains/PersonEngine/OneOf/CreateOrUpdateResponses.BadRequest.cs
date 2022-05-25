namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class CreateOrUpdateResponses
    {
        public sealed class BadRequest
        {
            public BadRequest(string message)
            {
                this.Message = message;
            }

            public string Message { get; set; }
        }
    }
}
