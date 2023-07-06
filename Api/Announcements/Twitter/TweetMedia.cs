using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TweetMedia
{
    [JsonProperty("media")]
    public List<TweetMediaData> Urls { get; set; }
}