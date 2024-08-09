using Microsoft.AspNetCore.Mvc;
using Mongo.API.Models;
using Mongo.API.Services;

namespace Mongo.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class JokeController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public JokeController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Joke>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Joke joke)
    {
        await _mongoDBService.CreateAsync(joke);
        // return Ok();
        return CreatedAtAction(nameof(Get), new { id = joke.Id }, joke);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Joke joke)
    {
        await _mongoDBService.UpdateAsync(id, joke);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return Ok();
    }
}
