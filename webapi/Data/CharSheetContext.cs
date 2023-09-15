using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DND;

namespace webapi.Data
{
    public class CharSheetContext : DbContext
    {
        public CharSheetContext(DbContextOptions<CharSheetContext> options) : base(options) { }
        public DbSet<User> BaseCharacters { get; set; }
        public DbSet<DndCharacter> DNDCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DndCharacter>()
                .HasOne(dnd => dnd.User)
                .WithMany(user => user.DNDCharacters)
                .HasForeignKey(dnd => dnd.UserToken)
                .HasPrincipalKey(user => user.UserToken); 
        }
    }
}
