using Microsoft.EntityFrameworkCore;
using myproject;
using myproject.Interface;
using myproject.Endpoints;



public class MusicRepo : IMusicRepo
{
    private readonly MusicContext _context;

    public MusicRepo(MusicContext context)
    {
        _context = context;
    }

    public async Task<Music> AddMusic(Music music)
    {
        var existingMusic = await _context.Music.FirstOrDefaultAsync(m => m.SongName == music.SongName);
        if (existingMusic == null)
        {
            music.Id = 0; // Ensure EF Core treats it as a new record
            await _context.Music.AddAsync(music);
            await _context.SaveChangesAsync();
            return music;
        }
        else
        {
            throw new Exception($"A song with the name '{music.SongName}' already exists.");
        }
    }

    public async Task<Music> UpdateMusic(int id, MusicUpdate updatedMusic)
    {
        var existingMusic = await _context.Music.FindAsync(id);

        if (existingMusic == null)
        {
            throw new KeyNotFoundException($"Music with Id {id} not found.");
        }

        // Update fields if they are explicitly provided in the DTO
        if (!string.IsNullOrWhiteSpace(updatedMusic.SongName))
        {
            existingMusic.SongName = updatedMusic.SongName;
        }
        if (!string.IsNullOrWhiteSpace(updatedMusic.Author))
        {
            existingMusic.Author = updatedMusic.Author;
        }

        await _context.SaveChangesAsync();
        return existingMusic;
    }

    public async Task<bool> DeleteMusic(int id)
    {
        var music = await _context.Music.FindAsync(id);
        if (music == null)
        {
            return false; // Music not found
        }

        _context.Music.Remove(music);
        await _context.SaveChangesAsync();
        return true; // Music deleted successfully
    }

    public async Task<IEnumerable<Music>> GetAllMusics()
    {
        return await _context.Music.ToListAsync();
    }

    public async Task<Music?> GetMusicById(int id)
    {
        return await _context.Music.FindAsync(id);
    }

    public async Task<IEnumerable<Music>> GetAllInStock()
    {
        // Assuming "Stock" was mistakenly referenced in the original code.
        // If "Stock" is not part of the Music class, remove this method or adjust accordingly.
        throw new NotImplementedException("Stock-related functionality needs clarification.");
    }

    public async Task<Music?> GetInStockById(int id)
    {
        // Same as above, adjust according to the actual data structure.
        throw new NotImplementedException("Stock-related functionality needs clarification.");
    }
}
