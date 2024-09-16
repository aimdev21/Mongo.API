using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mongo.API.Models;

public class Joke
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string JokeQuestion { get; set; } = null!;
  // [BsonElement("items")]
  // [JsonPropertyName("items")]
    public string JokeAnswer { get; set; } = null!;
}
