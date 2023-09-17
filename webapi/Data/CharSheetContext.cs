using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;

namespace webapi.Data
{
    public class CharSheetContext : DbContext
    {
        public CharSheetContext(DbContextOptions<CharSheetContext> options) : base(options) { }
        public DbSet<User> BaseCharacters { get; set; }
        public DbSet<BaseCharacter> DNDCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseCharacter>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<DndCharacter>("DND")
                .HasValue<BaseCharacter>("BASE");

            modelBuilder.Entity<BaseCharacter>()
                .HasOne(bc => bc.User)
                .WithMany(user => user.Characters)
                .HasForeignKey(bc => bc.UserToken)
                .HasPrincipalKey(user => user.UserToken);
        }
    }
}
