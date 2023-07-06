using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnnouncementFunctions.Announcements.Utils;

public static class AzureEnvironmentHelper
{
    public static IEnumerable<string> GetProfiles(string variableName)
    {
        var json = Environment.GetEnvironmentVariable(variableName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
        return data["profiles"];
    }
}