using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace AnnouncementFunctions.Announcements.Twitter;

public class TwitterClient : ITwitterClient
{
    private readonly RestClient _client;
    private readonly string _bearerToken;

    public TwitterClient(string bearerToken)
    {
        _client = new RestClient("https://api.twitter.com/2");
        _bearerToken = bearerToken;
    }
    
    public async Task<TwitterUser> GetTwitterUser(string username)
    {
        var request = new RestRequest($"/users/by/username/{username}?user.fields=profile_image_url");
        request.AddHeader("Content-type", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await _client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<TwitterUser>(response.Content);
    }

    public async Task<List<Tweet>> GetUserTweets(TwitterUser user, string tweetsAmount, string startTime, string endTime)
    {
        var amount = int.Parse(tweetsAmount);
        
        if (amount > 100)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be greater than 100!");
        
        var request = new RestRequest($"/users/{user.Data.Id}/tweets?max_results={tweetsAmount}&start_time={startTime}&end_time={endTime}&tweet.fields=created_at,referenced_tweets,author_id&expansions=attachments.media_keys&media.fields=preview_image_url,url");
        
        //https://api.twitter.com/2/users/:id/
        request.AddHeader("Content-type", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await _client.ExecuteAsync(request);
        
        var tweetCollection = JsonConvert.DeserializeObject<TweetCollection>(response.Content);

        var tweets = new List<Tweet>();

        foreach (var tweetData in tweetCollection.Data)
        {
            if (tweetData.ReferencedTweets != null)
                continue;
            
            var tweet = new Tweet
            {
                Id = tweetData.Id,
                Text = tweetData.Text, 
                CreatedAt = tweetData.CreatedAt,
                Author = new TwitterUserData
                {
                    Id = user.Data.Id,
                    Name = user.Data.Name,
                    Username = user.Data.Username,
                    AvatarImageUrl = user.Data.AvatarImageUrl
                },
                Media = tweetData.Attachments != null ? tweetCollection.Media.Urls.
                    Where(x => x.MediaKey == tweetData.Attachments.MediaKeys.FirstOrDefault())
                    .ToList() : null
            };
            
            tweets.Add(tweet);
        }
        
        return tweets;
    }
}