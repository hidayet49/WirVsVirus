using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeVsVirus.DataAccess.Migrations
{
    public partial class swabjob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwabJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(nullable: false),
                    CompletionTime = table.Column<DateTimeOffset>(nullable: true),
                    State = table.Column<byte>(nullable: false),
                    DriverAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwabJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwabJob_DriverAccounts_DriverAccountId",
                        column: x => x.DriverAccountId,
                        principalTable: "DriverAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SwabJobMatch",
                columns: table => new
                {
                    DriverAccountId = table.Column<int>(nullable: false),
                    SwabJobId = table.Column<int>(nullable: false),
                    MatchTimeStamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwabJobMatch", x => new { x.SwabJobId, x.DriverAccountId });
                    table.ForeignKey(
                        name: "FK_SwabJobMatch_DriverAccounts_DriverAccountId",
                        column: x => x.DriverAccountId,
                        principalTable: "DriverAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SwabJobMatch_SwabJob_SwabJobId",
                        column: x => x.SwabJobId,
                        principalTable: "SwabJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SwabJob_DriverAccountId",
                table: "SwabJob",
                column: "DriverAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SwabJobMatch_DriverAccountId",
                table: "SwabJobMatch",
                column: "DriverAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwabJobMatch");

            migrationBuilder.DropTable(
                name: "SwabJob");
        }
    }
}
