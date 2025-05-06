using LiteDB;
using TesteBenchmarkDotNet.Models;

namespace TesteBenchmarkDotNet.Service;

public class LiteDbCacheService
{
    private readonly ILiteCollection<TestData> _collection;

    public LiteDbCacheService()
    {
        var db = new LiteDatabase("Filename=cache.db;Mode=Shared");
        _collection = db.GetCollection<TestData>("cache");
    }

    public void Set(string key, TestData data)
    {
        //data = data with { Id = int.Parse(key) };
        _collection.Upsert(data);
    }

    public TestData? Get(string key)
    {
        //int id = int.Parse(key);
        return _collection.FindById(key);
    }
}
