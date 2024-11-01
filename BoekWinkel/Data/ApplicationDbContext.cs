using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoekWinkel.Models;

namespace BoekWinkel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BoekWinkel.Models.BoekModel> BoekModel { get; set; } = default!;
        public DbSet<BoekWinkel.Models.VoorRaadBoeken> VoorRaadBoeken { get; set; } = default!;
        public DbSet<BoekWinkel.Models.Winkelwagen> Winkelwagen { get; set; } = default!;    
        public DbSet<BoekWinkel.Models.VerlanglijstModel> VerlanglijstModel { get; set; } = default!;
        public DbSet<BoekWinkel.Models.UserMoneyModel> UserMoneyModel { get; set; } = default!;
    }
}
