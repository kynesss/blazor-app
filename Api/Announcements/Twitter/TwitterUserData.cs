using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TwitterUserData
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("profile_image_url")]
    public string AvatarImageUrl { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}