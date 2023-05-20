using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigortam.Cerit.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceCompanyId",
                table: "Insurance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_InsuranceCompanyId",
                table: "Insurance",
                column: "InsuranceCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompany",
                principalColumn: "InsuranceCompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_InsuranceCompany_InsuranceCompanyId",
                table: "Insurance");

            migrationBuilder.DropIndex(
                name: "IX_Insurance_InsuranceCompanyId",
                table: "Insurance");

            migrationBuilder.DropColumn(
                name: "InsuranceCompanyId",
                table: "Insurance");
        }
    }
}
