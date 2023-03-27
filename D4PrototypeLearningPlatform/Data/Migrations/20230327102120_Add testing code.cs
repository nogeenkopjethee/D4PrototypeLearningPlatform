using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Data.Migrations
{
    public partial class Addtestingcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestingCode",
                table: "Opgave",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestingCode",
                table: "Opgave");
        }
    }
}
