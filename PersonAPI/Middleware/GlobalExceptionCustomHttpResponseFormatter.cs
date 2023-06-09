using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Linq;
using System.Net;

namespace PersonAPI.Middleware
{
    public class GlobalExceptionCustomHttpResponseFormatter : DefaultHttpResponseFormatter
    {
        protected override HttpStatusCode OnDetermineStatusCode(
        IQueryResult result, FormatInfo format,
        HttpStatusCode? proposedStatusCode)
        {
            if (result.Errors?.Count > 0 &&
                result.Errors.Any(error => error.Code == "NotEmptyValidator"))
            {
                return HttpStatusCode.BadRequest;
            }

            return base.OnDetermineStatusCode(result, format, proposedStatusCode);
        }
    }
}
