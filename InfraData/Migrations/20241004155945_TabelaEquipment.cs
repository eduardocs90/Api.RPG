using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfraData.Migrations
{
    /// <inheritdoc />
    public partial class TabelaEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_CharacterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CharacterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Characters",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Class = table.Column<int>(type: "integer", nullable: true),
                    Damage = table.Column<int>(type: "integer", nullable: true),
                    Resistance = table.Column<int>(type: "integer", nullable: true),
                    Effect = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_CharacterId",
                table: "Equipments",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Characters_UserId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                table: "Users",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_CharacterId",
                table: "Users",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
