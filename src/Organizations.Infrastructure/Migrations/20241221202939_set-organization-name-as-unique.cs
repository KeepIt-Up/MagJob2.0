﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class setorganizationnameasunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Name",
                table: "Organizations",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_Name",
                table: "Organizations");
        }
    }
}
