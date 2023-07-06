using System.Threading.Tasks;

namespace AnnouncementFunctions.Storage.State;

public interface ITableClient<T>
{
    Task InsertOrUpdateData(string username, T data);
    Task<T> GetData(string username);
}