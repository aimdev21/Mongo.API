using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.API.DTOs;

public class JokeDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string JokeQuestion { get; set; } = null!;
    public string JokeAnswer { get; set; } = null!;
}