using OneOf;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.TokenRequestResponses;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class TokenRequestResponses : OneOfBase<Success, NotFound>
    {
        public TokenRequestResponses(OneOf<Success, NotFound> input)
            : base(input)
        {
        }

        public static implicit operator TokenRequestResponses(Success success)
            => new TokenRequestResponses(success);

        public static implicit operator TokenRequestResponses(NotFound notFound)
            => new TokenRequestResponses(notFound);

    }
}
