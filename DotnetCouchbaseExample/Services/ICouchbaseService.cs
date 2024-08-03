using Couchbase.KeyValue;

public interface ICouchbaseService
{
    Task InsertAsync(string id, object document);
    Task UpsertAsync(string id, object document);
    Task<IGetResult> GetAsync(string id);
    Task RemoveAsync(string id);
}
