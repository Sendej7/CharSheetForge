using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DNDCharacter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<int>(type: "int", nullable: false),
                    Alignment = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Dexterity = table.Column<int>(type: "int", nullable: false),
                    Constitution = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Wisdom = table.Column<int>(type: "int", nullable: false),
                    Charisma = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    ArmorClass = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Backstory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    SystemType = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DNDCharacter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AllyAndOrganization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustLevel = table.Column<int>(type: "int", nullable: false),
                    DNDCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllyAndOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllyAndOrganization_DNDCharacter_DNDCharacterID",
                        column: x => x.DNDCharacterID,
                        principalTable: "DNDCharacter",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AttackAndSpellcasting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    DamageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Components = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNDCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackAndSpellcasting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttackAndSpellcasting_DNDCharacter_DNDCharacterID",
                        column: x => x.DNDCharacterID,
                        principalTable: "DNDCharacter",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: true),
                    ArmorClass = table.Column<int>(type: "int", nullable: true),
                    SpecialProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNDCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_DNDCharacter_DNDCharacterID",
                        column: x => x.DNDCharacterID,
                        principalTable: "DNDCharacter",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FeatureAndTrait",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequired = table.Column<int>(type: "int", nullable: false),
                    DNDCharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureAndTrait", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureAndTrait_DNDCharacter_DNDCharacterID",
                        column: x => x.DNDCharacterID,
                        principalTable: "DNDCharacter",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllyAndOrganization_DNDCharacterID",
                table: "AllyAndOrganization",
                column: "DNDCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_AttackAndSpellcasting_DNDCharacterID",
                table: "AttackAndSpellcasting",
                column: "DNDCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_DNDCharacterID",
                table: "Equipment",
                column: "DNDCharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureAndTrait_DNDCharacterID",
                table: "FeatureAndTrait",
                column: "DNDCharacterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllyAndOrganization");

            migrationBuilder.DropTable(
                name: "AttackAndSpellcasting");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "FeatureAndTrait");

            migrationBuilder.DropTable(
                name: "DNDCharacter");
        }
    }
}
