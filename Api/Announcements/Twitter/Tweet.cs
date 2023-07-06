using System;
using System.Collections.Generic;

namespace AnnouncementFunctions.Announcements.Twitter;

public class Tweet
{
    public string Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public TwitterUserData Author { get; set; }
    public List<TweetMediaData> Media { get; set; }
}