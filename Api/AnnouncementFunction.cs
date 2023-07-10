using System;
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

    private const string FunctionUrl = "http://localhost:7071/api/CustomMediaPostsTrigger";

    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _httpClient = new HttpClient();

        var announcementUrl = await req.ReadAsStringAsync();

        if (string.IsNullOrEmpty(announcementUrl))
            return req.CreateResponse(HttpStatusCode.BadRequest);
        
        var urlWithQuery = $"{FunctionUrl}?url={Uri.EscapeDataString(announcementUrl)}";

        HttpContent content = new StringContent(urlWithQuery);
        await _httpClient.PostAsync(FunctionUrl, content);

        return req.CreateResponse(HttpStatusCode.OK);
    }
}