using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class upd_invitation_add_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvitedId",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedId",
                table: "Invitations");
        }
    }
}
