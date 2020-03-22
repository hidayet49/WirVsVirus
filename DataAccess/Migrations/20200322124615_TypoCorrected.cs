using Microsoft.EntityFrameworkCore.Migrations;

namespace WeVsVirus.DataAccess.Migrations
{
    public partial class TypoCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "PatientAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "PatientAccounts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "PatientAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AdressId",
                table: "PatientAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
