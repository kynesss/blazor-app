using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AnnouncementFunctions.Storage.Media;

public class AzureMediaStorageClient : IMediaStorageClient
{
    private readonly BlobContainerClient _containerClient;
    private readonly HttpClient _httpClient;
    
    public AzureMediaStorageClient(string connectionString, string blobContainerName)
    {
        _containerClient = new BlobContainerClient(connectionString, blobContainerName);
        _httpClient = new HttpClient();
    }

    public async Task<string> UploadMedia(string mediaName, string url)
    {
        await using var mediaStream = await GetMediaStream(url);
        return await Upload(mediaStream, mediaName);
    }

    private async Task<Stream> GetMediaStream(string url)
    {
        var downloadStream = await _httpClient.GetStreamAsync(url);
        return downloadStream;
    }
    
    private async Task<string> Upload(Stream mediaStream, string name)
    {
        var blob = _containerClient.GetBlobClient(name);
        await blob.UploadAsync(mediaStream, true);
        return blob.Uri.AbsoluteUri;
    }
}