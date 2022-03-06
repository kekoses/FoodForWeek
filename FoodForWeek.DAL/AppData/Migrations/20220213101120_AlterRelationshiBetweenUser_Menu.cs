using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodForWeekApp.DAL.AppData.Migrations
{
    public partial class AlterRelationshiBetweenUser_Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Menu_UserId",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_UserId",
                table: "Menu",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Menu_UserId",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_UserId",
                table: "Menu",
                column: "UserId",
                unique: true);
        }
    }
}
