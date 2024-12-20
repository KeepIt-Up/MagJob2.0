using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizations.API.Migrations
{
    /// <inheritdoc />
    public partial class removefieldsfromorganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Members",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
