using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AnnouncementFunctions.Announcements;

public class AnnouncementClientData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public AnnouncementType Type { get; set; }
    public List<string> Media { get; set; }
    public AnnouncementAuthor Author { get; set; }
    public AnnouncementAction Action { get; set; }
    public bool IsPinned { get; set; }
}

public class AnnouncementAction
{
    public string Url { get; set; }
    public string Text { get; set; }
}

public class AnnouncementAuthor
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}

public enum AnnouncementType
{
    Generic,
    Instagram,
    Twitter,
}