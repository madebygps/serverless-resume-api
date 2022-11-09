using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetResume
    {
        private readonly ILogger _logger;

        public GetResume(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetResume>();
        }

        [Function("GetResume")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [BlobInput("resume/myresume.json")] string myResume)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.WriteString(myResume);

            return response;
        }
    }
}
