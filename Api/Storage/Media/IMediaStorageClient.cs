using System.Threading.Tasks;

namespace AnnouncementFunctions.Storage.Media;

public interface IMediaStorageClient
{
    public Task<string> UploadMedia(string mediaName, string url);
}
    