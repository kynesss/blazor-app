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

    //url
    private const string FunctionUrl = "http://localhost:7071/api/CustomMediaPostsTrigger?url=";

    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _httpClient = new HttpClient();
        
        var logger = executionContext.GetLogger("AnnouncementFunction");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var query = HttpUtility.ParseQueryString(req.Url.Query);
        var url = query.Get("url");
        
        //var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{FunctionUrl}{url}");
        //var httpResponse = await _httpClient.SendAsync(httpRequest);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync($"{FunctionUrl}{url}");
        
        return response;
    }
}