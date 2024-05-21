using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class ChangeNameOfCell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bmployeePicture",
                table: "Employees",
                newName: "employeePicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employeePicture",
                table: "Employees",
                newName: "bmployeePicture");
        }
    }
}
