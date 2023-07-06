using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramApiSharp.Classes.Models;

namespace AnnouncementFunctions.Instagram;

public interface IInstagramClient
{
    Task<List<InstaMedia>> GetUserPosts(string username);
}