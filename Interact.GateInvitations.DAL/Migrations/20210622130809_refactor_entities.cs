using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class refactor_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityKeepers_Users_UserId",
                table: "SecurityKeepers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SecurityKeepers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_Id",
                table: "Customers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityKeepers_Users_Id",
                table: "SecurityKeepers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityKeepers_Users_Id",
                table: "SecurityKeepers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SecurityKeepers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityKeepers_Users_UserId",
                table: "SecurityKeepers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
