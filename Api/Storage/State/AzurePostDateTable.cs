using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace AnnouncementFunctions.Storage.State;

public class AzurePostDateTable : ITableClient<DateTime>
{
    private readonly CloudTable _table;

    public AzurePostDateTable(string storageConnectionString, string tableName)
    {
        var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
        var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

        _table = tableClient.GetTableReference(tableName);
    }
    
    public async Task InsertOrUpdateData(string username, DateTime dateTime)
    {
        var entity = new AnnouncementEntity(username, "rowkey")
        {
            CreationDate = dateTime.Date
        };

        var mergeOperation = TableOperation.InsertOrMerge(entity);
        await _table.ExecuteAsync(mergeOperation);
    }

    public async Task<DateTime> GetData(string username)
    {
        var retrieveOperation = TableOperation.Retrieve<AnnouncementEntity>(username, "rowkey");

        var result = await _table.ExecuteAsync(retrieveOperation);
        var entity = result.Result as AnnouncementEntity;

        return entity?.CreationDate ?? DateTime.MinValue;
    }
}