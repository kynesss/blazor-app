using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Beamable;

public class BeamableAuthRequest
{
    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("grant_type")]
    public string GrantType { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
    
    [JsonProperty("customerScoped")]
    public bool CustomerScoped { get; set; }
}