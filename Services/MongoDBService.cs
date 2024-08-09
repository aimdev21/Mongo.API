using Microsoft.Extensions.Options;
using Mongo.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace Mongo.API.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Joke> _playlistCollection;
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient context = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = context.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Joke>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Joke joke)
    {
        await _playlistCollection.InsertOneAsync(joke);
    }

    public async Task<List<Joke>> GetAsync()
    {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateAsync(string id, Joke joke)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        UpdateDefinition<Joke> update = Builders<Joke>.Update.Set(x => x.JokeQuestion, joke.JokeQuestion).Set(x => x.JokeAnswer, joke.JokeAnswer);
        await _playlistCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
    }
}