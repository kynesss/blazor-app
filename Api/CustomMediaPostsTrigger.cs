using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AnnouncementFunctions.Announcements;
using AnnouncementFunctions.Announcements.Beamable;
using AnnouncementFunctions.Announcements.CustomMedia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AnnouncementFunctions;

public static class CustomMediaPostsTrigger
{
    private static IAnnouncementClient _announcementClient;
    private static ICustomMediaClient _mediaClient;

    [FunctionName("CustomMediaPostsTrigger")]
    public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ClaimsPrincipal principal)
    {
        if (principal.Identity is { IsAuthenticated: false })
        {
            log.LogError("Request was not authenticated!");
            throw new UnauthorizedAccessException();
        }
        
        var url = req.Query["url"];
        
        var beamableLogin = Environment.GetEnvironmentVariable("BEAMABLE_LOGIN");
        var beamablePassword = Environment.GetEnvironmentVariable("BEAMABLE_PASSWORD");
        var beamableXdeScope = Environment.GetEnvironmentVariable("BEAMABLE_XDESCOPE");

        _announcementClient = new BeamableAnnouncementClient(beamableXdeScope, beamableLogin, beamablePassword);
        _mediaClient = new CustomMediaClient();

        var mediaPost = await _mediaClient.GetCustomPost(url);

        if (mediaPost == null)
            throw new Exception("Media post is null!");
        
        var announcement = CustomAnnouncementFactory.Create(mediaPost);
        await _announcementClient.PostAnnouncement(announcement);

        return new OkResult();
    }
}