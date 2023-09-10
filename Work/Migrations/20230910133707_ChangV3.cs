using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginComponent.Migrations
{
    /// <inheritdoc />
    public partial class ChangV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "Packeg");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Packeg_ParcelId1",
                        column: x => x.ParcelId1,
                        principalTable: "Packeg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParcelId1",
                table: "Location",
                column: "ParcelId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "Packeg",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
