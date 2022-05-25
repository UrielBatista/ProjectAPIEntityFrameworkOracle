using OneOf;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.DeleteResponses;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class DeleteResponses : OneOfBase<Success, BadRequest>
    {
        public DeleteResponses(OneOf<Success, BadRequest> input)
            : base(input)
        {
        }

        public static implicit operator DeleteResponses(Success success)
            => new DeleteResponses(success);

        public static implicit operator DeleteResponses(BadRequest badRequest)
            => new DeleteResponses(badRequest);
    }
}
