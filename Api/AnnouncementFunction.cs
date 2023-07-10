using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json.Linq;

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

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var requestModel = JObject.Parse(requestBody);
        var url = requestModel.Value<string>("url");
        
        var urlWithQuery = $"{FunctionUrl}url={Uri.EscapeDataString(url)}";
        
        var response = await _httpClient.GetAsync(urlWithQuery);
        return req.CreateResponse(response.StatusCode);
    }
}