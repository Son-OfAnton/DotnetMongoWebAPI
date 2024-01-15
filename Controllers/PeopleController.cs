using Microsoft.AspNetCore.Mvc;
using DotnetMongoWebAPI.Services;
using DotnetMongoWebAPI.Models;

namespace DotnetMongoWebAPI.Controllers;

[Controller]
[Route("api/[Controller]")]
public class PeopleController : Controller
{
  private readonly MongoDBService _mongoDBService;

  public PeopleController(MongoDBService mongoDBService)
  {
    _mongoDBService = mongoDBService;
  }

  [HttpGet]
  public async Task<List<People>> GetAllPeople()
  {
    return await _mongoDBService.GetAllPeople();
  }

  [HttpPost]
  public async Task<IActionResult> AddPerson([FromBody] People person)
  {
    await _mongoDBService.AddPerson(person);
    return CreatedAtAction(nameof(GetAllPeople), new { id = person.Id }, person);

  }

  [HttpPut("{id}")]
  public async Task<IActionResult> EditPerson(string id, [FromBody] People Person)
  {
    await _mongoDBService.EditPerson(id, Person);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeletePerson(string id)
  {
    await _mongoDBService.DeletePerson(id);
    return NoContent();
  }
}