using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.DND;
using webapi.Models.DND.Enums;
using webapi.Models.Generics;

namespace webapi.Data
{
    public class CharSheetContext : DbContext
    {
        public CharSheetContext(DbContextOptions<CharSheetContext> options) : base(options) { }
        public DbSet<User> BaseCharacters { get; set; }
        public DbSet<BaseCharacter> DNDCharacters { get; set; }
        public DbSet<CharacterAssociation<Equipment>> EquipmentAssociations { get; set; }
        public DbSet<CharacterAssociation<AllyAndOrganization>> AllyAndOrganizationAssociations { get; set; }
        public DbSet<CharacterAssociation<AttackAndSpellcasting>> AttacksAndSpellcastingAssociations { get; set; }
        public DbSet<CharacterAssociation<FeatureAndTrait>> FeaturesAndTraitsAssociations { get; set; }

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

            modelBuilder.Entity<CharacterAssociation<Equipment>>()
            .HasKey(ca => new { ca.DndCharacterId, ca.AssociableId });
            modelBuilder.Entity<CharacterAssociation<Equipment>>()
               .HasOne(ca => ca.DndCharacter)
               .WithMany(dc => dc.EquipmentAssociations)
               .HasForeignKey(ca => ca.DndCharacterId);

            // Relacja many-to-many dla AllyAndOrganization
            modelBuilder.Entity<CharacterAssociation<AllyAndOrganization>>()
               .HasKey(ca => new { ca.DndCharacterId, ca.AssociableId });
            modelBuilder.Entity<CharacterAssociation<AllyAndOrganization>>()
               .HasOne(ca => ca.DndCharacter)
               .WithMany(dc => dc.AllyAndOrganizationAssociations)
               .HasForeignKey(ca => ca.DndCharacterId);

            // Relacja many-to-many dla AttackAndSpellcasting
            modelBuilder.Entity<CharacterAssociation<AttackAndSpellcasting>>()
               .HasKey(ca => new { ca.DndCharacterId, ca.AssociableId });
            modelBuilder.Entity<CharacterAssociation<AttackAndSpellcasting>>()
               .HasOne(ca => ca.DndCharacter)
               .WithMany(dc => dc.AttacksAndSpellcastingAssociations)
               .HasForeignKey(ca => ca.DndCharacterId);

            // Relacja many-to-many dla FeatureAndTrait
            modelBuilder.Entity<CharacterAssociation<FeatureAndTrait>>()
               .HasKey(ca => new { ca.DndCharacterId, ca.AssociableId });
            modelBuilder.Entity<CharacterAssociation<FeatureAndTrait>>()
               .HasOne(ca => ca.DndCharacter)
               .WithMany(dc => dc.FeaturesAndTraitsAssociations)
               .HasForeignKey(ca => ca.DndCharacterId);

        }
    }
}
