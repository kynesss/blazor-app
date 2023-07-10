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
        
        var functionUrl = "https://newsfeedfunctions.azurewebsites.net/api/CustomMediaPostsTrigger?";
        var queryParameter = "https://przegladsportowy.onet.pl/tenis/wimbledon/najtrudniejszy-test-hurkacza-dogrywka-oto-jak-pokonac-djokovicia/w2vjgfj";
        var urlWithQuery = $"{functionUrl}?url={Uri.EscapeDataString(queryParameter)}";
        
        var response = await _httpClient.GetAsync(urlWithQuery);
        
        Console.WriteLine($"Response status code: {response.StatusCode}");
        Console.WriteLine($"Response content: {await response.Content.ReadAsStringAsync()}");
        
        return req.CreateResponse(response.StatusCode);
    }
}