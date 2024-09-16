using Microsoft.AspNetCore.Mvc;
using Mongo.API.DTOs;
using Mongo.API.Services;

namespace Mongo.API.Controllers;

[Controller]
[ApiController]
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
        try
        {
            return await _mongoDBService.GetAsync();
        }
        catch (Exception)
        {

            throw new Exception("No entries found to display!");
        }
    }

    [HttpGet("{id}")]
    public async Task<JokeDTO> GetJokeById(string id)
    {
        try
        {
            return await _mongoDBService.GetJokeByIdAsync(id);
        }
        catch (Exception)
        {
            throw new Exception("No Joke found in the DB");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] JokeDTO jokeDTO)
    {
        try
        {
            await _mongoDBService.CreateAsync(jokeDTO);
            // return Ok();
            return CreatedAtAction(nameof(Get), new { id = jokeDTO.Id }, jokeDTO);
        }
        catch (Exception)
        {

            throw new Exception("The Joke has not been created!");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] JokeDTO jokeDTO)
    {
        try
        {
            await _mongoDBService.UpdateAsync(jokeDTO);
            return Ok();
        }
        catch (Exception)
        {
            throw new Exception("The Joke has not been updated!");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _mongoDBService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception)
        {

            throw new Exception("The Joke has not been deleted successfully!");
        }
    }
}
