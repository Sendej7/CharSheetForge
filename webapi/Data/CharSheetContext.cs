using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DND;

namespace webapi.Data
{
    public class CharSheetContext : DbContext
    {
        public CharSheetContext(DbContextOptions<CharSheetContext> options) : base(options) { }
        public DbSet<User> BaseCharacters { get; set; }
        public DbSet<DNDCharacter> DNDCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DNDCharacter>()
                .HasOne(dnd => dnd.User)
                .WithMany(baseChar => baseChar.DNDCharacters)
                .HasForeignKey(dnd => dnd.UserToken);
        }
    }
}
