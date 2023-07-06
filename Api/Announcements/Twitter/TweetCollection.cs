using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TweetCollection
{
    [JsonProperty("data")]
    public List<TweetData> Data { get; set; }

    [JsonProperty("includes")]
    public TweetMedia Media { get; set; }
}