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
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
        HttpRequestData req, FunctionContext executionContext)
    {
        HttpClient client = new HttpClient();

        string functionUrl = "http://localhost:7071/api/CustomMediaPostsTrigger";
        string queryParameter =
            "https://www.onet.pl/informacje/onetwiadomosci/rzez-wolynska-kleska-dyplomatyczna-i-moralna-andrzeja-dudy/dtztlx3,79cfc278";
        string urlWithQuery = $"{functionUrl}?url={Uri.EscapeDataString(queryParameter)}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(urlWithQuery);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed with exception: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}