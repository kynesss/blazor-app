using System.Threading.Tasks;

namespace AnnouncementFunctions.Announcements.CustomMedia;

public interface ICustomMediaClient
{
    Task<CustomMediaPost> GetCustomPost(string url);
}