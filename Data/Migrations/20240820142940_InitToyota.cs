using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToyotaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitToyota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameColor = table.Column<string>(type: "TEXT", nullable: true),
                    CodeColor = table.Column<string>(type: "TEXT", nullable: true),
                    UrlColor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToyotaModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameModel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToyotaModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplectationModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameComplectation = table.Column<string>(type: "TEXT", nullable: true),
                    ModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplectationModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplectationModels_ToyotaModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "ToyotaModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplectationColorModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MainImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ColorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComplectationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplectationColorModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplectationColorModels_ColorModels_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ColorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplectationColorModels_ComplectationModels_ComplectationId",
                        column: x => x.ComplectationId,
                        principalTable: "ComplectationModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplectationColorModels_ColorId",
                table: "ComplectationColorModels",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplectationColorModels_ComplectationId",
                table: "ComplectationColorModels",
                column: "ComplectationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplectationModels_ModelId",
                table: "ComplectationModels",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplectationColorModels");

            migrationBuilder.DropTable(
                name: "ColorModels");

            migrationBuilder.DropTable(
                name: "ComplectationModels");

            migrationBuilder.DropTable(
                name: "ToyotaModels");
        }
    }
}
