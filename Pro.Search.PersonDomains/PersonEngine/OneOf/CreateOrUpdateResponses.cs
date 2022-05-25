using OneOf;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdateResponses;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class CreateOrUpdateResponses : OneOfBase<Success, NotFound, BadRequest>
    {
        public CreateOrUpdateResponses(OneOf<Success, NotFound, BadRequest> input)
            : base(input)
        {
        }

        public static implicit operator CreateOrUpdateResponses(Success success)
            => new CreateOrUpdateResponses(success);

        public static implicit operator CreateOrUpdateResponses(NotFound notFound)
            => new CreateOrUpdateResponses(notFound);

        public static implicit operator CreateOrUpdateResponses(BadRequest badRequest)
            => new CreateOrUpdateResponses(badRequest);
    }
}
