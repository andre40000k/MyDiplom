using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginComponent.Migrations
{
    /// <inheritdoc />
    public partial class ChangV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Packeg_ParcelId1",
                table: "Location");

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

            migrationBuilder.DropIndex(
                name: "IX_Location_ParcelId1",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packeg",
                table: "Packeg");

            migrationBuilder.DropColumn(
                name: "ParcelId1",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Packeg",
                newName: "Parcel");

            migrationBuilder.RenameIndex(
                name: "IX_Packeg_UserId",
                table: "Parcel",
                newName: "IX_Parcel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Packeg_TransportId",
                table: "Parcel",
                newName: "IX_Parcel_TransportId");

            migrationBuilder.RenameIndex(
                name: "IX_Packeg_RegionalDepartmentId",
                table: "Parcel",
                newName: "IX_Parcel_RegionalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Packeg_LocalDepartmentId",
                table: "Parcel",
                newName: "IX_Parcel_LocalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Packeg_DistrictDepartmentId",
                table: "Parcel",
                newName: "IX_Parcel_DistrictDepartmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Parcel",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParcelId",
                table: "Location",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Parcel_ParcelId",
                table: "Location",
                column: "ParcelId",
                principalTable: "Parcel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_DistrictDepartment_DistrictDepartmentId",
                table: "Parcel",
                column: "DistrictDepartmentId",
                principalTable: "DistrictDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_LocalDepartment_LocalDepartmentId",
                table: "Parcel",
                column: "LocalDepartmentId",
                principalTable: "LocalDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_RegionalDepartment_RegionalDepartmentId",
                table: "Parcel",
                column: "RegionalDepartmentId",
                principalTable: "RegionalDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_Transport_TransportId",
                table: "Parcel",
                column: "TransportId",
                principalTable: "Transport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_User_UserId",
                table: "Parcel",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Parcel_ParcelId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_DistrictDepartment_DistrictDepartmentId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_LocalDepartment_LocalDepartmentId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_RegionalDepartment_RegionalDepartmentId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_Transport_TransportId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_User_UserId",
                table: "Parcel");

            migrationBuilder.DropIndex(
                name: "IX_Location_ParcelId",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel");

            migrationBuilder.RenameTable(
                name: "Parcel",
                newName: "Packeg");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_UserId",
                table: "Packeg",
                newName: "IX_Packeg_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_TransportId",
                table: "Packeg",
                newName: "IX_Packeg_TransportId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_RegionalDepartmentId",
                table: "Packeg",
                newName: "IX_Packeg_RegionalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_LocalDepartmentId",
                table: "Packeg",
                newName: "IX_Packeg_LocalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_DistrictDepartmentId",
                table: "Packeg",
                newName: "IX_Packeg_DistrictDepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "ParcelId1",
                table: "Location",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Packeg",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packeg",
                table: "Packeg",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParcelId1",
                table: "Location",
                column: "ParcelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Packeg_ParcelId1",
                table: "Location",
                column: "ParcelId1",
                principalTable: "Packeg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }
    }
}
