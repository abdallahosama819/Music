using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myproject.Interface
{
    public interface IMusicRepo
    {
        Task<Music> AddMusic(Music music);
        Task<Music> UpdateMusic(int id, MusicUpdate updatedMusic);
        Task<bool> DeleteMusic(int id);
        Task<IEnumerable<Music>> GetAllMusics();
        Task<Music?> GetMusicById(int id);
        Task<IEnumerable<Music>> GetAllInStock();
        Task<Music?> GetInStockById(int id);
    }
}
