﻿// <auto-generated />
using System;
using Interact.GateInvitations.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Interact.GateInvitations.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210701123454_upd_invitation_add_prop")]
    partial class upd_invitation_add_prop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserStatus")
                        .HasColumnType("int");

                    b.Property<string>("VillaNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvitedId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvitedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCodeImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.InviteeLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HandlerSecurityKeeperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LoginStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("RelatedInvitationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HandlerSecurityKeeperId");

                    b.HasIndex("RelatedInvitationId");

                    b.ToTable("InviteeLogins");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.SecurityKeeper", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SecurityKeepers");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Admin", b =>
                {
                    b.HasOne("Interact.GateInvitations.Core.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Customer", b =>
                {
                    b.HasOne("Interact.GateInvitations.Core.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Invitation", b =>
                {
                    b.HasOne("Interact.GateInvitations.Core.Data.Customer", "Customer")
                        .WithMany("Invitations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.InviteeLogin", b =>
                {
                    b.HasOne("Interact.GateInvitations.Core.Data.SecurityKeeper", "HandlerSecurityKeeper")
                        .WithMany("InvitationsLogins")
                        .HasForeignKey("HandlerSecurityKeeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Interact.GateInvitations.Core.Data.Invitation", "RelatedInvitation")
                        .WithMany()
                        .HasForeignKey("RelatedInvitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HandlerSecurityKeeper");

                    b.Navigation("RelatedInvitation");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.SecurityKeeper", b =>
                {
                    b.HasOne("Interact.GateInvitations.Core.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.Customer", b =>
                {
                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("Interact.GateInvitations.Core.Data.SecurityKeeper", b =>
                {
                    b.Navigation("InvitationsLogins");
                });
#pragma warning restore 612, 618
        }
    }
}
