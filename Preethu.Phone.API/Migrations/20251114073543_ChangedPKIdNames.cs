using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preethu.Phone.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPKIdNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TblSpecification",
                newName: "SpecId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TblManufacturer",
                newName: "MId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecId",
                table: "TblSpecification",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MId",
                table: "TblManufacturer",
                newName: "Id");
        }
    }
}
