using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class add_three_props_to_customerTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrl",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestrictNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentUrl",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DestrictNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Customers");
        }
    }
}
