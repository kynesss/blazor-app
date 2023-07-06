using System.Threading.Tasks;

namespace AnnouncementFunctions.Announcements;

public interface IAnnouncementClient
{
    Task PostAnnouncement(Announcement announcement);
}