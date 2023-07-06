using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementFunctions.Announcements.Twitter;

public interface ITwitterClient
{
    Task<TwitterUser> GetTwitterUser(string username);
    Task<List<Tweet>> GetUserTweets(TwitterUser user, string tweetsAmount, string startTime, string endTime);
}