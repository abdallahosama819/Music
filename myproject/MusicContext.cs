using Microsoft.EntityFrameworkCore;

namespace myproject.Endpoints
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }

        // DbSet for Music
        public DbSet<Music> Music { get; set; }
    }
}