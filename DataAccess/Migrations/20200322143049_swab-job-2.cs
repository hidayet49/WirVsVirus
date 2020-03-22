using Microsoft.EntityFrameworkCore.Migrations;

namespace WeVsVirus.DataAccess.Migrations
{
    public partial class swabjob2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientAccountId",
                table: "SwabJob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SwabJob_PatientAccountId",
                table: "SwabJob",
                column: "PatientAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SwabJob_PatientAccounts_PatientAccountId",
                table: "SwabJob",
                column: "PatientAccountId",
                principalTable: "PatientAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwabJob_PatientAccounts_PatientAccountId",
                table: "SwabJob");

            migrationBuilder.DropIndex(
                name: "IX_SwabJob_PatientAccountId",
                table: "SwabJob");

            migrationBuilder.DropColumn(
                name: "PatientAccountId",
                table: "SwabJob");
        }
    }
}
