using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class ALterNoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
