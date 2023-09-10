using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginComponent.Migrations
{
    /// <inheritdoc />
    public partial class ChangV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_User_UserId1",
                table: "Packeg");

            migrationBuilder.DropIndex(
                name: "IX_Packeg_UserId1",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Packeg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packeg_UserId1",
                table: "Packeg",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_User_UserId1",
                table: "Packeg",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
