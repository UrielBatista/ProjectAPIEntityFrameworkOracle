using GraphQL;
using GraphQL.Transport;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using PersonAPI.GraphQL;
using System;
using System.Threading.Tasks;

namespace PersonAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IDocumentExecuter documentExecuter;
        private readonly ISchema schema;

        public ApiController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            this.documentExecuter = documentExecuter;
            this.schema = schema;
        }

        [HttpPost("graphql")]
        public async Task<IActionResult> GraphQL([FromBody] GraphQLRequest request)
        {
            var startTime = DateTime.UtcNow;
            var result = await this.documentExecuter.ExecuteAsync(x =>
            {
                x.Schema = this.schema;
                x.Query = request.Query;
                x.Variables = request.Variables;
                x.OperationName = request.OperationName;
                x.RequestServices = this.HttpContext.RequestServices;
                x.CancellationToken = this.HttpContext.RequestAborted;
                x.UserContext = new GraphQLUserContext { User = this.HttpContext.User };
            });

            return new ExecutionResultActionResult(result);
        }

    }
}
