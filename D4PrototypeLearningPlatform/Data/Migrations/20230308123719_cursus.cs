using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Data.Migrations
{
    public partial class cursus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CursusId",
                table: "Module",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cursus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Module_CursusId",
                table: "Module",
                column: "CursusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Cursus_CursusId",
                table: "Module",
                column: "CursusId",
                principalTable: "Cursus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Cursus_CursusId",
                table: "Module");

            migrationBuilder.DropTable(
                name: "Cursus");

            migrationBuilder.DropIndex(
                name: "IX_Module_CursusId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "CursusId",
                table: "Module");
        }
    }
}
