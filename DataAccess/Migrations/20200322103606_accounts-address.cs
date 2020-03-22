using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace WeVsVirus.DataAccess.Migrations
{
    public partial class accountsaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Birthday",
                table: "DriverAccounts",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ExpoToken",
                table: "DriverAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "DriverAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "DriverAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StreetAndNumber = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    GeoLocation = table.Column<Point>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInstituteAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInstituteAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInstituteAccounts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalInstituteAccounts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTimeOffset>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    AdressId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAccounts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientAccounts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInstituteAccounts_AddressId",
                table: "MedicalInstituteAccounts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInstituteAccounts_AppUserId",
                table: "MedicalInstituteAccounts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAccounts_AddressId",
                table: "PatientAccounts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAccounts_AppUserId",
                table: "PatientAccounts",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalInstituteAccounts");

            migrationBuilder.DropTable(
                name: "PatientAccounts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "DriverAccounts");

            migrationBuilder.DropColumn(
                name: "ExpoToken",
                table: "DriverAccounts");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "DriverAccounts");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "DriverAccounts");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
