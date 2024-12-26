﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Organizations.Infrastructure.Persistence;

#nullable disable

namespace Organizations.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20241221152400_init")]
partial class init
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "9.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("MemberRole", b =>
            {
                b.Property<Guid>("MembersID")
                    .HasColumnType("uuid");

                b.Property<Guid>("RolesID")
                    .HasColumnType("uuid");

                b.HasKey("MembersID", "RolesID");

                b.HasIndex("RolesID");

                b.ToTable("MemberRole");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Invitation", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTimeOffset>("DateCreated")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateDeleted")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateUpdated")
                    .HasColumnType("timestamp with time zone");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<Guid>("OrganizationId")
                    .HasColumnType("uuid");

                b.Property<int>("Status")
                    .HasColumnType("integer");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

                b.HasKey("ID");

                b.HasIndex("OrganizationId");

                b.ToTable("Invitations");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Member", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<bool>("Archived")
                    .HasColumnType("boolean");

                b.Property<DateTimeOffset>("DateCreated")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateDeleted")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateUpdated")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("FirstName")
                    .HasColumnType("text");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<string>("LastName")
                    .HasColumnType("text");

                b.Property<string>("Notes")
                    .HasColumnType("text");

                b.Property<Guid>("OrganizationId")
                    .HasColumnType("uuid");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

                b.HasKey("ID");

                b.HasIndex("OrganizationId");

                b.ToTable("Members");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Organization", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<bool>("Archived")
                    .HasColumnType("boolean");

                b.Property<byte[]>("BannerImage")
                    .HasColumnType("bytea");

                b.Property<DateTimeOffset>("DateCreated")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateDeleted")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateUpdated")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("OwnerId")
                    .HasColumnType("uuid");

                b.Property<byte[]>("ProfileImage")
                    .HasColumnType("bytea");

                b.HasKey("ID");

                b.ToTable("Organizations");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Permission", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTimeOffset>("DateCreated")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateDeleted")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateUpdated")
                    .HasColumnType("timestamp with time zone");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("ID");

                b.ToTable("Permissions");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Role", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("Color")
                    .HasColumnType("text");

                b.Property<DateTimeOffset>("DateCreated")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateDeleted")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTimeOffset?>("DateUpdated")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("boolean");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<Guid>("OrganizationId")
                    .HasColumnType("uuid");

                b.HasKey("ID");

                b.HasIndex("OrganizationId");

                b.ToTable("Roles");
            });

        modelBuilder.Entity("PermissionRole", b =>
            {
                b.Property<Guid>("PermissionsID")
                    .HasColumnType("uuid");

                b.Property<Guid>("RolesID")
                    .HasColumnType("uuid");

                b.HasKey("PermissionsID", "RolesID");

                b.HasIndex("RolesID");

                b.ToTable("PermissionRole");
            });

        modelBuilder.Entity("MemberRole", b =>
            {
                b.HasOne("Organizations.Domain.Entities.Member", null)
                    .WithMany()
                    .HasForeignKey("MembersID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Organizations.Domain.Entities.Role", null)
                    .WithMany()
                    .HasForeignKey("RolesID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Invitation", b =>
            {
                b.HasOne("Organizations.Domain.Entities.Organization", "Organization")
                    .WithMany("Invitations")
                    .HasForeignKey("OrganizationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Organization");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Member", b =>
            {
                b.HasOne("Organizations.Domain.Entities.Organization", "Organization")
                    .WithMany("Members")
                    .HasForeignKey("OrganizationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Organization");
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Role", b =>
            {
                b.HasOne("Organizations.Domain.Entities.Organization", "Organization")
                    .WithMany("Roles")
                    .HasForeignKey("OrganizationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Organization");
            });

        modelBuilder.Entity("PermissionRole", b =>
            {
                b.HasOne("Organizations.Domain.Entities.Permission", null)
                    .WithMany()
                    .HasForeignKey("PermissionsID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Organizations.Domain.Entities.Role", null)
                    .WithMany()
                    .HasForeignKey("RolesID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Organizations.Domain.Entities.Organization", b =>
            {
                b.Navigation("Invitations");

                b.Navigation("Members");

                b.Navigation("Roles");
            });
#pragma warning restore 612, 618
    }
}
