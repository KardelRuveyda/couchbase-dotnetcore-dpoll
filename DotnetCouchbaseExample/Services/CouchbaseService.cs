using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Couchbase;

public class CouchbaseService : ICouchbaseService
{
    private readonly IBucket _bucket;
    private readonly ICouchbaseCollection _collection;

    public CouchbaseService(IBucketProvider bucketProvider)
    {
        _bucket = bucketProvider.GetBucketAsync("travel-sample").GetAwaiter().GetResult();
        var scope = _bucket.Scope("futuverse");
        _collection = scope.Collection("UserInfos");
    }

    public async Task InsertAsync(string id, object document)
    {
        await _collection.InsertAsync(id, document);
    }

    public async Task UpsertAsync(string id, object document)
    {
        await _collection.UpsertAsync(id, document);
    }

    public async Task<IGetResult> GetAsync(string id)
    {
        return await _collection.GetAsync(id);
    }

    public async Task RemoveAsync(string id)
    {
        await _collection.RemoveAsync(id);
    }
}
