using System;
using System.Security.Cryptography;
using System.Text;

namespace AnnouncementFunctions.Announcements.Utils;

public static class Sha256
{
    public static string GetStringSha256Hash(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        
        var textData = Encoding.UTF8.GetBytes(text);
        var hash = SHA256.HashData(textData);
        
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }
}