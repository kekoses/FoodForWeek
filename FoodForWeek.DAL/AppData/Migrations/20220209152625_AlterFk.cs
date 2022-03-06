using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodForWeekApp.DAL.AppData.Migrations
{
    public partial class AlterFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");
        }
    }
}
