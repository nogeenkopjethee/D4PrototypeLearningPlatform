using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrolement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Cursus",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CursusId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpgaveId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursus_ApplicationUserId",
                table: "Cursus",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursus_AspNetUsers_ApplicationUserId",
                table: "Cursus",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursus_AspNetUsers_ApplicationUserId",
                table: "Cursus");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropIndex(
                name: "IX_Cursus_ApplicationUserId",
                table: "Cursus");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Cursus");
        }
    }
}
