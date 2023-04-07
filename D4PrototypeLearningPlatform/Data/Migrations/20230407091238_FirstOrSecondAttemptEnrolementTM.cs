using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class FirstOrSecondAttemptEnrolementTM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursus_AspNetUsers_ApplicationUserId",
                table: "Cursus");

            migrationBuilder.DropIndex(
                name: "IX_Cursus_ApplicationUserId",
                table: "Cursus");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Cursus");

            migrationBuilder.CreateTable(
                name: "EnroledCurses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CursusId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnroledCurses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnroledCurses");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Cursus",
                type: "text",
                nullable: true);

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
    }
}
