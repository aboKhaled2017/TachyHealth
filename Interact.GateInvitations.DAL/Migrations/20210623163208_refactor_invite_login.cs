using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class refactor_invite_login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InviteeLogins_Customers_RelatedInviterId",
                table: "InviteeLogins");

            migrationBuilder.RenameColumn(
                name: "RelatedInviterId",
                table: "InviteeLogins",
                newName: "RelatedInvitationId");

            migrationBuilder.RenameIndex(
                name: "IX_InviteeLogins_RelatedInviterId",
                table: "InviteeLogins",
                newName: "IX_InviteeLogins_RelatedInvitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InviteeLogins_Invitations_RelatedInvitationId",
                table: "InviteeLogins",
                column: "RelatedInvitationId",
                principalTable: "Invitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InviteeLogins_Invitations_RelatedInvitationId",
                table: "InviteeLogins");

            migrationBuilder.RenameColumn(
                name: "RelatedInvitationId",
                table: "InviteeLogins",
                newName: "RelatedInviterId");

            migrationBuilder.RenameIndex(
                name: "IX_InviteeLogins_RelatedInvitationId",
                table: "InviteeLogins",
                newName: "IX_InviteeLogins_RelatedInviterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InviteeLogins_Customers_RelatedInviterId",
                table: "InviteeLogins",
                column: "RelatedInviterId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
