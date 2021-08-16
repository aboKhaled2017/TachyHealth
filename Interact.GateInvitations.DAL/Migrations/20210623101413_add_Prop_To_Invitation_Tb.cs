using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class add_Prop_To_Invitation_Tb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeImgUrl",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeImgUrl",
                table: "Invitations");
        }
    }
}
