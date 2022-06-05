using System.Collections.Generic;
using System.Security.Claims;

namespace PersonAPI.GraphQL
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; } = default!;
    }
}
