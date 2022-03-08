using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodForWeek.DAL.AppData.Migrations
{
    public partial class AlterColumnLoginToUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "User",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "Login");
        }
    }
}
