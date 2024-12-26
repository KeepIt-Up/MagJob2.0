using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizations.Infrastructure.Migrations;

/// <inheritdoc />
public partial class init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Organizations",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true),
                Archived = table.Column<bool>(type: "boolean", nullable: false),
                OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                ProfileImage = table.Column<byte[]>(type: "bytea", nullable: true),
                BannerImage = table.Column<byte[]>(type: "bytea", nullable: true),
                DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DateDeleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Organizations", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Permissions",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DateDeleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Permissions", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Invitations",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uuid", nullable: false),
                OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DateDeleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Invitations", x => x.ID);
                table.ForeignKey(
                    name: "FK_Invitations_Organizations_OrganizationId",
                    column: x => x.OrganizationId,
                    principalTable: "Organizations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Members",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "text", nullable: true),
                LastName = table.Column<string>(type: "text", nullable: true),
                Notes = table.Column<string>(type: "text", nullable: true),
                Archived = table.Column<bool>(type: "boolean", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DateDeleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Members", x => x.ID);
                table.ForeignKey(
                    name: "FK_Members_Organizations_OrganizationId",
                    column: x => x.OrganizationId,
                    principalTable: "Organizations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: true),
                Color = table.Column<string>(type: "text", nullable: true),
                OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                DateDeleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.ID);
                table.ForeignKey(
                    name: "FK_Roles_Organizations_OrganizationId",
                    column: x => x.OrganizationId,
                    principalTable: "Organizations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MemberRole",
            columns: table => new
            {
                MembersID = table.Column<Guid>(type: "uuid", nullable: false),
                RolesID = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemberRole", x => new { x.MembersID, x.RolesID });
                table.ForeignKey(
                    name: "FK_MemberRole_Members_MembersID",
                    column: x => x.MembersID,
                    principalTable: "Members",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MemberRole_Roles_RolesID",
                    column: x => x.RolesID,
                    principalTable: "Roles",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PermissionRole",
            columns: table => new
            {
                PermissionsID = table.Column<Guid>(type: "uuid", nullable: false),
                RolesID = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsID, x.RolesID });
                table.ForeignKey(
                    name: "FK_PermissionRole_Permissions_PermissionsID",
                    column: x => x.PermissionsID,
                    principalTable: "Permissions",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PermissionRole_Roles_RolesID",
                    column: x => x.RolesID,
                    principalTable: "Roles",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Invitations_OrganizationId",
            table: "Invitations",
            column: "OrganizationId");

        migrationBuilder.CreateIndex(
            name: "IX_MemberRole_RolesID",
            table: "MemberRole",
            column: "RolesID");

        migrationBuilder.CreateIndex(
            name: "IX_Members_OrganizationId",
            table: "Members",
            column: "OrganizationId");

        migrationBuilder.CreateIndex(
            name: "IX_PermissionRole_RolesID",
            table: "PermissionRole",
            column: "RolesID");

        migrationBuilder.CreateIndex(
            name: "IX_Roles_OrganizationId",
            table: "Roles",
            column: "OrganizationId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Invitations");

        migrationBuilder.DropTable(
            name: "MemberRole");

        migrationBuilder.DropTable(
            name: "PermissionRole");

        migrationBuilder.DropTable(
            name: "Members");

        migrationBuilder.DropTable(
            name: "Permissions");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropTable(
            name: "Organizations");
    }
}
