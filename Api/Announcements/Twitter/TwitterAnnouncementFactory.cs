using System.Collections.Generic;
using System.Linq;

namespace AnnouncementFunctions.Announcements.Twitter;

public static class TwitterAnnouncementFactory
{
    public static Announcement Create(Tweet tweet, IEnumerable<string> mediaUrls)
    {
        var announcement = new Announcement
        {
            Symbol = tweet.Id,
            Title = "",
            Summary = "Twitter post",
            Body = tweet.Text,
            ClientData = CreateClientData(tweet, mediaUrls),
            Channel = "any"
        };

        return announcement;
    }

    private static AnnouncementClientData CreateClientData(Tweet tweet, IEnumerable<string> mediaUrls)
    {
        var clientData = new AnnouncementClientData
        {
            Type = AnnouncementType.Twitter,
            Author = new AnnouncementAuthor
            {
                Name = tweet.Author.Username,
                ImageUrl = tweet.Author.AvatarImageUrl
            },
            Action = new AnnouncementAction
            {
                Url = $"https://twitter.com/{tweet.Author.Username}/status/{tweet.Id}",
                Text = "$newsfeed_twitter_cta"
            },
            Media = mediaUrls.ToList(),
            IsPinned = false
        };

        return clientData;
    }
}