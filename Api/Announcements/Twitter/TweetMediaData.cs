using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TweetMediaData
{
    [JsonProperty("media_key")]
    public string MediaKey { get; set; }
    
    [JsonProperty("preview_image_url")]
    public string PreviewImageUrl { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
}