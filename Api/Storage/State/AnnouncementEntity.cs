using System;
using Microsoft.Azure.Cosmos.Table;

namespace AnnouncementFunctions.Storage.State;

public class AnnouncementEntity : TableEntity
{
    public AnnouncementEntity(string partitionKey, string rowKey)
    {
        PartitionKey = partitionKey;
        RowKey = rowKey;
    }

    public AnnouncementEntity() { }
    public DateTime CreationDate { get; set; }
}