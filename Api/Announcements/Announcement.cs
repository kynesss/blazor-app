using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements;

public class Announcement
{
    /// <summary>
    /// Beamable uses this to identify the announcement (has to be unique)
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Equivalent to the "Subject" of the web app editor
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("summary")] 
    public string Summary { get; set; }

    [JsonProperty("body")] 
    public string Body { get; set; }

    [JsonProperty("channel")] 
    public string Channel { get; set; }

    [JsonProperty("clientData")] 
    public AnnouncementClientData ClientData { get; set; }
}