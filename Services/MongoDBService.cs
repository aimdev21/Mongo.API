using AutoMapper;
using Microsoft.Extensions.Options;
using Mongo.API.DTOs;
using Mongo.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.API.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Joke> _jokeCollection;
    private readonly IMapper _mapper;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper)
    {
        MongoClient context = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = context.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _jokeCollection = database.GetCollection<Joke>(mongoDBSettings.Value.CollectionName);
        _mapper = mapper;
    }

    public async Task CreateAsync(JokeDTO jokeDTO)
    {
        var result = _mapper.Map<Joke>(jokeDTO);
        await _jokeCollection.InsertOneAsync(result);
    }

    public async Task<List<JokeDTO>> GetAsync()
    {
        var result = await _jokeCollection.Find(new BsonDocument()).ToListAsync();
        return _mapper.Map<List<JokeDTO>>(result);
    }

    public async Task<JokeDTO> GetJokeByIdAsync(string id)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        var result = await _jokeCollection.Find(filter).FirstOrDefaultAsync();
        return _mapper.Map<JokeDTO>(result);
    }

    public async Task UpdateAsync(JokeDTO jokeDTO)
    {
        var filter = Builders<Joke>.Filter.Eq("Id", jokeDTO.Id);
        var update = Builders<Joke>.Update.Set(x => x.JokeQuestion, jokeDTO.JokeQuestion).Set(x => x.JokeAnswer, jokeDTO.JokeAnswer);
        await _jokeCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        await _jokeCollection.DeleteOneAsync(filter);
    }
}