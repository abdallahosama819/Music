using Microsoft.AspNetCore.Mvc;
using myproject;
using myproject.Interface;

namespace myproject.Endpoints;


[ApiController]
[Route("api/music")]
public class MusicController : ControllerBase
{
    private readonly IMusicRepo _musicRepo;

    public MusicController(IMusicRepo musicRepo)
    {
        _musicRepo = musicRepo;
    }

    [HttpPost("AddMusic")]
    public async Task<IActionResult> AddMusic([FromBody] Music music)
    {
        if (music == null)
        {
            return BadRequest(new { message = "Invalid music data provided." });
        }

        try
        {
            var addedMusic = await _musicRepo.AddMusic(music);
            return CreatedAtAction(nameof(GetMusicById), new { id = addedMusic.Id }, addedMusic);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpDelete("DeleteMusic/{id}")]
    public async Task<IActionResult> DeleteMusic(int id)
    {
        var result = await _musicRepo.DeleteMusic(id);

        if (!result)
        {
            return NotFound(new { message = "Music not found with the provided ID." });
        }

        return Ok(new { message = "Music deleted successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMusic(int id, [FromBody] MusicUpdate updatedMusic)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updated = await _musicRepo.UpdateMusic(id, updatedMusic);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    [HttpGet("GetAllMusic")]
    public async Task<IActionResult> GetAllMusic()
    {
        var musics = await _musicRepo.GetAllMusics();
        return Ok(musics);
    }

    [HttpGet("GetMusicById/{id}")]
    public async Task<IActionResult> GetMusicById(int id)
    {
        var music = await _musicRepo.GetMusicById(id);

        if (music == null)
        {
            return NotFound(new { message = "Music not found with the provided ID." });
        }

        return Ok(music);
    }

    [HttpGet("GetAllMusicInStock")]
    public async Task<IActionResult> GetAllInStockMusic()
    {
        var musicsInStock = await _musicRepo.GetAllInStock();
        return Ok(musicsInStock);
    }

    [HttpGet("GetMusicInStockById/{id}")]
    public async Task<IActionResult> GetMusicInStockById(int id)
    {
        var music = await _musicRepo.GetInStockById(id);
        if (music == null)
        {
            return NotFound(new { message = "Music not found or out of stock." });
        }

        return Ok(music);
    }
}
