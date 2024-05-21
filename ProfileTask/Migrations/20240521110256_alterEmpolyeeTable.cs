using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class alterEmpolyeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employeePicture",
                table: "Employees",
                newName: "employeePicturePath");

            migrationBuilder.RenameColumn(
                name: "backgroundPicture",
                table: "Employees",
                newName: "backgroundPicturePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employeePicturePath",
                table: "Employees",
                newName: "employeePicture");

            migrationBuilder.RenameColumn(
                name: "backgroundPicturePath",
                table: "Employees",
                newName: "backgroundPicture");
        }
    }
}
