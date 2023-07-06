using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TwitterUser
{
    [JsonProperty("data")]
    public TwitterUserData Data { get; set; }
}