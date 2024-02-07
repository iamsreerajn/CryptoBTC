using CryptoBTC.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBTC.Data
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options):base  (options)
        {

        }
        public DbSet<CandlesBTC> CandlesBTC{ get; set; }
        public DbSet<UpdateTracker> UpdateTracker { get; set; }
    }
}
