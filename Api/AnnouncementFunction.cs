using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated;

public static class AnnouncementFunction
{
    private static HttpClient _httpClient;

    private const string FunctionUrl = "http://localhost:7071/api/CustomMediaPostsTrigger?url=";

    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        //url jest nullem dlatego nie dziala
        _httpClient = new HttpClient();

        var logger = executionContext.GetLogger("AnnouncementFunction");
        logger.LogInformation("C# HTTP trigger function processed a request.");
        
        var announcementUrl = await req.ReadAsStringAsync();

        if (string.IsNullOrEmpty(announcementUrl))
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var url = FunctionUrl + announcementUrl;
        await _httpClient.PostAsync(url, null);

        return req.CreateResponse(HttpStatusCode.OK);
    }
}