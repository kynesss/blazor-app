using System.Collections.Generic;
using System.Linq;
using AnnouncementFunctions.Announcements;
using InstagramApiSharp.Classes.Models;

namespace AnnouncementFunctions.Instagram;

public static class InstagramAnnouncementFactory
{
    public static Announcement Create(InstaMedia instagramPost, IEnumerable<string> mediaUrls)
    {
        var announcement = new Announcement
        {
            Symbol = instagramPost.InstaIdentifier,
            Title = instagramPost.Title ?? "",
            Summary = "Instagram post",
            Body = instagramPost.Caption?.Text,
            ClientData = CreateClientData(instagramPost, mediaUrls),
            Channel = "any"
        };

        return announcement;
    }

    private static AnnouncementClientData CreateClientData(InstaMedia instagramPost, IEnumerable<string> mediaUrls)
    {
        var clientData = new AnnouncementClientData
        {
            Type = AnnouncementType.Instagram,
            Author = new AnnouncementAuthor
            {
                Name = instagramPost.User.UserName,
                ImageUrl = instagramPost.User.ProfilePicUrl
            },
            Action = new AnnouncementAction
            {
                Url = "https://www.instagram.com/p/" + instagramPost.Code,
                Text = "$newsfeed_instagram_cta"
            },
            Media = mediaUrls.ToList(),
            IsPinned = false
        };

        return clientData;
    }
}