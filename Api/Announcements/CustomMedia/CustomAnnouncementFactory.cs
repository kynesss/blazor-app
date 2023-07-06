using System.Collections.Generic;
using AnnouncementFunctions.Announcements.Utils;

namespace AnnouncementFunctions.Announcements.CustomMedia;

public static class CustomAnnouncementFactory
{
    public static Announcement Create(CustomMediaPost post)
    {
        var announcement = new Announcement
        {
            Symbol = Sha256.GetStringSha256Hash(post.Url),
            Title = post.Title,
            Summary = "Custom post",
            Body = post.Description,
            ClientData = CreateClientData(post.Url, post.ImageUrl),
            Channel = "any"
        };

        return announcement;
    }

    private static AnnouncementClientData CreateClientData(string url, string imageUrl)
    {
        var clientData = new AnnouncementClientData
        {
            Type = AnnouncementType.Generic,
            Action = new AnnouncementAction
            {
                Url = url,
                Text = "$newsfeed_custom_cta"
            },
            Media = new List<string>
            {
                {imageUrl}
            },
            IsPinned = false
        };

        return clientData;
    }
}