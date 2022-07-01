using OneOf;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdatePickerListResponses;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class CreateOrUpdatePickerListResponses : OneOfBase<Success>
    {
        public CreateOrUpdatePickerListResponses(OneOf<Success> input)
            : base(input)
        {
        }

        public static implicit operator CreateOrUpdatePickerListResponses(Success success)
            => new CreateOrUpdatePickerListResponses(success);
    }
}
