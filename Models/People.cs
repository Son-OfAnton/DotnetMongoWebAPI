using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetMongoWebAPI.Models;

public class People
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
}