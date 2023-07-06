using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace AnnouncementFunctions.Announcements.Beamable;

public class BeamableAnnouncementClient : IAnnouncementClient
{
    private readonly string _xdeScope;
    private readonly string _login;
    private readonly string _password;
    
    private string _accessToken;

    public BeamableAnnouncementClient(string xdeScope, string login, string password)
    {
        _xdeScope = xdeScope;
        _login = login;
        _password = password;
    }
    
    public async Task PostAnnouncement(Announcement announcement)
    {
        if (string.IsNullOrEmpty(_accessToken))
        {
            var beamableAuth = new BeamableAuthRequest
            {
                Username = _login,
                GrantType = "password",
                Password = _password,
                CustomerScoped = true
            };
            
            _accessToken = await GetAccessToken(beamableAuth);
        }

        var json = JsonConvert.SerializeObject(announcement);
        var client = new RestClient("https://api.beamable.com/basic/announcements/");
        
        var request = new RestRequest("", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("X-DE-SCOPE", _xdeScope);
        request.AddHeader("authorization", _accessToken);
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        
        await client.ExecuteAsync(request);
    }

    private async Task<string> GetAccessToken(BeamableAuthRequest beamableAuthRequest)
    {
        var json = JsonConvert.SerializeObject(beamableAuthRequest);
        
        var client = new RestClient("https://api.beamable.com/basic/auth/token");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("X-DE-SCOPE", _xdeScope);
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        
        var response = await client.ExecuteAsync(request);

        if (response.Content == null)
            throw new Exception("Access Token is Null!");
        
        var content = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
        return $"{content["token_type"]} {content["access_token"]}";
    }
}