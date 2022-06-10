namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class TokenRequestResponses
    {
        public sealed class NotFound
        {
            public NotFound(string message)
            {
                this.Message = message;
            }

            public string Message { get; set; }
        }
    }
}
