using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileTask.Migrations
{
    public partial class alterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adddress",
                table: "contacts",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "contacts",
                newName: "Adddress");
        }
    }
}
