using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class alterotherTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "educations",
                newName: "EducPicturePath");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "backgrounds",
                newName: "BackgroundPicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducPicturePath",
                table: "educations",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "BackgroundPicture",
                table: "backgrounds",
                newName: "Picture");
        }
    }
}
