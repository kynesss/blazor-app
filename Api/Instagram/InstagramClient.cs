using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;

namespace AnnouncementFunctions.Instagram;

public class InstagramClient : IInstagramClient
{
    private readonly IInstaApi _api;

    public InstagramClient(string username, string password)
    {
        var userSession = new UserSessionData
        {
            UserName = username,
            Password = password
        };

        _api = InstaApiBuilder.CreateBuilder()
            .SetUser(userSession)
            .UseLogger(new DebugLogger(LogLevel.Exceptions))
            .Build();
    }

    private async Task<bool> Login()
    {
        var loginRequest = await _api.LoginAsync();
        return loginRequest.Succeeded;
    }

    public async Task<List<InstaMedia>> GetUserPosts(string username)
    {
        var isLoggedIn = await Login();

        if (!isLoggedIn)
            throw new Exception("You must be logged in to get user posts");

        var media = await _api.UserProcessor.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(1));
        return media.Value.ToList();
    }
}