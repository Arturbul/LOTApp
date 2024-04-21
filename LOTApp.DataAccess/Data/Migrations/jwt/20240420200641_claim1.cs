using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.data.ef.migrations.jwt
{
    /// <inheritdoc />
    public partial class claim1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightsAllowed",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "FlightsAllowed",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
