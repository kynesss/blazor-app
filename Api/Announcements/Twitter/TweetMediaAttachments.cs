using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TweetMediaAttachments
{
    [JsonProperty("media_keys")]
    public List<string> MediaKeys { get; set; }
}