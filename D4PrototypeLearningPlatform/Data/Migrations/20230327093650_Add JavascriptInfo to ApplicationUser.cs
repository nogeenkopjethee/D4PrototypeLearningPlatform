using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Data.Migrations
{
    public partial class AddJavascriptInfotoApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "JavascriptInfo",
                table: "AspNetUsers",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JavascriptInfo",
                table: "AspNetUsers");
        }
    }
}
