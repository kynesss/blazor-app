using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace ApiIsolated;

public static class AnnouncementFunction
{
    private static HttpClient _httpClient;
    private const string FunctionUrl = "https://newsfeedfunctions.azurewebsites.net/api/CustomMediaPostsTrigger?";
    
    [Function("AnnouncementFunction")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _httpClient = new HttpClient();
        
        var queryParameters = QueryHelpers.ParseQuery(req.Url.Query);
        string url = queryParameters["url"];
        var urlWithQuery = $"{FunctionUrl}url={url}";
        
        var response = await _httpClient.GetAsync(urlWithQuery);
        return req.CreateResponse(response.StatusCode);
    }
}