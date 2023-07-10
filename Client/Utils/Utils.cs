namespace BlazorApp.Client.Utils;

public static class Utils
{
    public static bool IsUrlValid(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out var resultUri))
            return resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps;
        
        return false;
    }
}