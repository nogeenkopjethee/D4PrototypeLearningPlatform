using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D4PrototypeLearningPlatform.Migrations;

/// <inheritdoc />
public partial class ModuleWithOpgavenLink : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "ModuleId",
            table: "Opgave",
            type: "uuid",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Opgave_ModuleId",
            table: "Opgave",
            column: "ModuleId");

        migrationBuilder.AddForeignKey(
            name: "FK_Opgave_Module_ModuleId",
            table: "Opgave",
            column: "ModuleId",
            principalTable: "Module",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Opgave_Module_ModuleId",
            table: "Opgave");

        migrationBuilder.DropIndex(
            name: "IX_Opgave_ModuleId",
            table: "Opgave");

        migrationBuilder.DropColumn(
            name: "ModuleId",
            table: "Opgave");
    }
}
