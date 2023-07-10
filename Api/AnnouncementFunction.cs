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
    
    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _httpClient = new HttpClient();
        
        var functionUrl = "http://localhost:7071/api/CustomMediaPostsTrigger";
        //var queryParameter = await req.ReadAsStringAsync();
        var queryParameter = "https://przegladsportowy.onet.pl/tenis/wimbledon/najtrudniejszy-test-hurkacza-dogrywka-oto-jak-pokonac-djokovicia/w2vjgfj";
        var urlWithQuery = $"{functionUrl}?url={Uri.EscapeDataString(queryParameter)}";

        //if (string.IsNullOrEmpty(queryParameter))
          //  return req.CreateResponse(HttpStatusCode.BadRequest);
        
        var response = await _httpClient.GetAsync(urlWithQuery);

        return req.CreateResponse(HttpStatusCode.OK);
    }
}