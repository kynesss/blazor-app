using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated;

public static class AnnouncementFunction
{
    private static HttpClient _httpClient;

    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _httpClient = new HttpClient();

        var logger = executionContext.GetLogger("AnnouncementFunction");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var query = HttpUtility.ParseQueryString(req.Url.Query);
        var url = query.Get("url");

        if (string.IsNullOrEmpty(url))
        {
            // Handle the case when "url" parameter is missing or empty
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        // Continue with your logic using the "url" parameter value

        return req.CreateResponse(HttpStatusCode.OK);
    }
}