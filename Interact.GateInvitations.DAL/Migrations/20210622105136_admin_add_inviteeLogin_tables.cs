using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.GateInvitations.DAL.Migrations
{
    public partial class admin_add_inviteeLogin_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InviteeLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginStatus = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandlerSecurityKeeperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedInviterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteeLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteeLogins_Customers_RelatedInviterId",
                        column: x => x.RelatedInviterId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InviteeLogins_SecurityKeepers_HandlerSecurityKeeperId",
                        column: x => x.HandlerSecurityKeeperId,
                        principalTable: "SecurityKeepers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InviteeLogins_HandlerSecurityKeeperId",
                table: "InviteeLogins",
                column: "HandlerSecurityKeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_InviteeLogins_RelatedInviterId",
                table: "InviteeLogins",
                column: "RelatedInviterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "InviteeLogins");
        }
    }
}
