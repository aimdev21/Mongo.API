using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Mongo.API.DTOs;

public class JokeDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [Required]
    public string JokeQuestion { get; set; } = null!;
    [Required]
    public string JokeAnswer { get; set; } = null!;
}