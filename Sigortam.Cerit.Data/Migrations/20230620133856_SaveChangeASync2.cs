using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigortam.Cerit.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaveChangeASync2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Vehicle",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Vehicle",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "User",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "User",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Insurance",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Insurance",
                newName: "CreatedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Vehicle",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Vehicle",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "User",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "User",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Insurance",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Insurance",
                newName: "CreatedDate");
        }
    }
}
