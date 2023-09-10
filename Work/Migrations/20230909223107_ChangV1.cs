using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginComponent.Migrations
{
    /// <inheritdoc />
    public partial class ChangV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_Transport_TransportId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_User_UserId",
                table: "Packeg");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictDepartmentId",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalDepartmentId",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionalDepartmentId",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packeg_DistrictDepartmentId",
                table: "Packeg",
                column: "DistrictDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Packeg_LocalDepartmentId",
                table: "Packeg",
                column: "LocalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Packeg_RegionalDepartmentId",
                table: "Packeg",
                column: "RegionalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Packeg_UserId1",
                table: "Packeg",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_DistrictDepartment_DistrictDepartmentId",
                table: "Packeg",
                column: "DistrictDepartmentId",
                principalTable: "DistrictDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_LocalDepartment_LocalDepartmentId",
                table: "Packeg",
                column: "LocalDepartmentId",
                principalTable: "LocalDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_RegionalDepartment_RegionalDepartmentId",
                table: "Packeg",
                column: "RegionalDepartmentId",
                principalTable: "RegionalDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_Transport_TransportId",
                table: "Packeg",
                column: "TransportId",
                principalTable: "Transport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_User_UserId",
                table: "Packeg",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_User_UserId1",
                table: "Packeg",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_DistrictDepartment_DistrictDepartmentId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_LocalDepartment_LocalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_RegionalDepartment_RegionalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_Transport_TransportId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_User_UserId",
                table: "Packeg");

            migrationBuilder.DropForeignKey(
                name: "FK_Packeg_User_UserId1",
                table: "Packeg");

            migrationBuilder.DropIndex(
                name: "IX_Packeg_DistrictDepartmentId",
                table: "Packeg");

            migrationBuilder.DropIndex(
                name: "IX_Packeg_LocalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropIndex(
                name: "IX_Packeg_RegionalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropIndex(
                name: "IX_Packeg_UserId1",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "DistrictDepartmentId",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "LocalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "RegionalDepartmentId",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Packeg");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransportId",
                table: "Packeg",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_Transport_TransportId",
                table: "Packeg",
                column: "TransportId",
                principalTable: "Transport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packeg_User_UserId",
                table: "Packeg",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
