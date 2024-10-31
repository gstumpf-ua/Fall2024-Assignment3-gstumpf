using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fall2024_Assignment3_gstumpf.Data.Migrations
{
    /// <inheritdoc />
    public partial class Reseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Year",
                value: 2006);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Year",
                value: 2008);
        }
    }
}
