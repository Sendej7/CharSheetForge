using Microsoft.EntityFrameworkCore;
using webapi.Models.DND;

namespace webapi.Data
{
    public class CharSheetContext : DbContext
    {
        public CharSheetContext(DbContextOptions<CharSheetContext> options) : base(options) { }
        public DbSet<DNDCharacter> DNDCharacter { get; set; } = null!;
    }
}
