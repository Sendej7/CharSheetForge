using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    TrustLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllyAndOrganization", x => x.Id);
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
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackAndSpellcasting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseCharacters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCharacters", x => x.ID);
                    table.UniqueConstraint("AK_BaseCharacters_UserToken", x => x.UserToken);
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
                    SpecialProperties = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
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
                    LevelRequired = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureAndTrait", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DNDCharacters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserToken = table.Column<int>(type: "int", nullable: false),
                    CardToken = table.Column<int>(type: "int", nullable: false),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    SystemType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<int>(type: "int", nullable: true),
                    Alignment = table.Column<int>(type: "int", nullable: true),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strength = table.Column<int>(type: "int", nullable: true),
                    Dexterity = table.Column<int>(type: "int", nullable: true),
                    Constitution = table.Column<int>(type: "int", nullable: true),
                    Intelligence = table.Column<int>(type: "int", nullable: true),
                    Wisdom = table.Column<int>(type: "int", nullable: true),
                    Charisma = table.Column<int>(type: "int", nullable: true),
                    HitPoints = table.Column<int>(type: "int", nullable: true),
                    ArmorClass = table.Column<int>(type: "int", nullable: true),
                    Speed = table.Column<int>(type: "int", nullable: true),
                    Initiative = table.Column<int>(type: "int", nullable: true),
                    Gold = table.Column<int>(type: "int", nullable: true),
                    Backstory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DNDCharacters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DNDCharacters_BaseCharacters_UserToken",
                        column: x => x.UserToken,
                        principalTable: "BaseCharacters",
                        principalColumn: "UserToken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllyAndOrganizationAssociations",
                columns: table => new
                {
                    DndCharacterId = table.Column<int>(type: "int", nullable: false),
                    AssociableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllyAndOrganizationAssociations", x => new { x.DndCharacterId, x.AssociableId });
                    table.ForeignKey(
                        name: "FK_AllyAndOrganizationAssociations_AllyAndOrganization_AssociableId",
                        column: x => x.AssociableId,
                        principalTable: "AllyAndOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllyAndOrganizationAssociations_DNDCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DNDCharacters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttacksAndSpellcastingAssociations",
                columns: table => new
                {
                    DndCharacterId = table.Column<int>(type: "int", nullable: false),
                    AssociableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttacksAndSpellcastingAssociations", x => new { x.DndCharacterId, x.AssociableId });
                    table.ForeignKey(
                        name: "FK_AttacksAndSpellcastingAssociations_AttackAndSpellcasting_AssociableId",
                        column: x => x.AssociableId,
                        principalTable: "AttackAndSpellcasting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttacksAndSpellcastingAssociations_DNDCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DNDCharacters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentAssociations",
                columns: table => new
                {
                    DndCharacterId = table.Column<int>(type: "int", nullable: false),
                    AssociableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentAssociations", x => new { x.DndCharacterId, x.AssociableId });
                    table.ForeignKey(
                        name: "FK_EquipmentAssociations_DNDCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DNDCharacters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentAssociations_Equipment_AssociableId",
                        column: x => x.AssociableId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeaturesAndTraitsAssociations",
                columns: table => new
                {
                    DndCharacterId = table.Column<int>(type: "int", nullable: false),
                    AssociableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesAndTraitsAssociations", x => new { x.DndCharacterId, x.AssociableId });
                    table.ForeignKey(
                        name: "FK_FeaturesAndTraitsAssociations_DNDCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DNDCharacters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeaturesAndTraitsAssociations_FeatureAndTrait_AssociableId",
                        column: x => x.AssociableId,
                        principalTable: "FeatureAndTrait",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllyAndOrganizationAssociations_AssociableId",
                table: "AllyAndOrganizationAssociations",
                column: "AssociableId");

            migrationBuilder.CreateIndex(
                name: "IX_AttacksAndSpellcastingAssociations_AssociableId",
                table: "AttacksAndSpellcastingAssociations",
                column: "AssociableId");

            migrationBuilder.CreateIndex(
                name: "IX_DNDCharacters_UserToken",
                table: "DNDCharacters",
                column: "UserToken");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssociations_AssociableId",
                table: "EquipmentAssociations",
                column: "AssociableId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesAndTraitsAssociations_AssociableId",
                table: "FeaturesAndTraitsAssociations",
                column: "AssociableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllyAndOrganizationAssociations");

            migrationBuilder.DropTable(
                name: "AttacksAndSpellcastingAssociations");

            migrationBuilder.DropTable(
                name: "EquipmentAssociations");

            migrationBuilder.DropTable(
                name: "FeaturesAndTraitsAssociations");

            migrationBuilder.DropTable(
                name: "AllyAndOrganization");

            migrationBuilder.DropTable(
                name: "AttackAndSpellcasting");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "DNDCharacters");

            migrationBuilder.DropTable(
                name: "FeatureAndTrait");

            migrationBuilder.DropTable(
                name: "BaseCharacters");
        }
    }
}
