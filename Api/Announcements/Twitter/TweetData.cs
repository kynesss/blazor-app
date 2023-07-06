using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TweetData
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("author_id")]
    public string AuthorId { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("attachments")]
    public TweetMediaAttachments Attachments { get; set; }
    
    [JsonProperty("referenced_tweets")]
    public List<Dictionary<string, string>> ReferencedTweets { get; set; }
}