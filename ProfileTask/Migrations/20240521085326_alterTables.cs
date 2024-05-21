using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class alterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrgnizeName",
                table: "educations",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "licenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "licenses");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "educations",
                newName: "OrgnizeName");
        }
    }
}
