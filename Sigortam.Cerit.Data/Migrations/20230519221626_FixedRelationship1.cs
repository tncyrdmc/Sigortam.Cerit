using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigortam.Cerit.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationship1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_User_UserId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_Vehicle_VehicleId",
                table: "Insurance");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Insurance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Insurance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceCompanyId",
                table: "Insurance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompany",
                principalColumn: "InsuranceCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_User_UserId",
                table: "Insurance",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_Vehicle_VehicleId",
                table: "Insurance",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_User_UserId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_Vehicle_VehicleId",
                table: "Insurance");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Insurance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Insurance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceCompanyId",
                table: "Insurance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompany",
                principalColumn: "InsuranceCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_User_UserId",
                table: "Insurance",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_Vehicle_VehicleId",
                table: "Insurance",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
