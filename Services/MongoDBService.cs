using DotnetMongoWebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DotnetMongoWebAPI.Services;

public class MongoDBService
{
  private readonly IMongoCollection<People> _peopleCollection;

  public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
  {
    MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
    IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
    _peopleCollection = database.GetCollection<People>(mongoDBSettings.Value.CollectionName);
  }

  public async Task<List<People>> GetAllPeople() {
    return await _peopleCollection.Find(new BsonDocument()).ToListAsync();
  }


  public async Task AddPerson(People person)
  {
    await _peopleCollection.InsertOneAsync(person);
    return;
  }

  public async Task EditPerson(string id, People Person)
  {
    FilterDefinition<People> filter = Builders<People>.Filter.Eq("Id", id);
    UpdateDefinition<People> update = Builders<People>.Update
      .Set("FirstName", Person.FirstName)
      .Set("LastName", Person.LastName);
    await _peopleCollection.UpdateOneAsync(filter, update);
    return;
  }

  public async Task DeletePerson(string id)
  {
    FilterDefinition<People> filter = Builders<People>.Filter.Eq("Id", id);
    await _peopleCollection.DeleteOneAsync(filter);
    return;
  }

}