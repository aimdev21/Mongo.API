using Microsoft.AspNetCore.Mvc;
using Mongo.API.DTOs;
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
    public async Task<List<JokeDTO>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<JokeDTO> GetJokeById(string id)
    {
        return await _mongoDBService.GetJokeByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] JokeDTO jokeDTO)
    {
        await _mongoDBService.CreateAsync(jokeDTO);
        // return Ok();
        return CreatedAtAction(nameof(Get), new { id = jokeDTO.Id }, jokeDTO);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] JokeDTO jokeDTO)
    {
        await _mongoDBService.UpdateAsync(jokeDTO);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return Ok();
    }
}
