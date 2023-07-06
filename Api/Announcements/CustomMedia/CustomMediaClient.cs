using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AnnouncementFunctions.Announcements.CustomMedia;

public class CustomMediaClient : ICustomMediaClient
{
    private readonly HttpClient _httpClient;
    private readonly HtmlDocument _htmlDocument;
    
    public CustomMediaClient()
    {
        _httpClient = new HttpClient();
        _htmlDocument = new HtmlDocument();
    }
    
    public async Task<CustomMediaPost> GetCustomPost(string url)
    {
        var html = await _httpClient.GetStringAsync(url);
        _htmlDocument.LoadHtml(html);
        
        var titleNode = _htmlDocument.DocumentNode.SelectSingleNode("//title");
        var descNode = _htmlDocument.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var imageNode = _htmlDocument.DocumentNode.SelectSingleNode("//meta[@property='og:image']");

        var title = titleNode.InnerText;
        var description = descNode.Attributes["content"].Value;
        var imageUrl = imageNode.Attributes["content"].Value;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(imageUrl))
            return null;
        
        return new CustomMediaPost
        {
            Title = title,
            Description = description,
            Url = url,
            ImageUrl = imageUrl
        };
    }
}