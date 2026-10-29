using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StajYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UserKilit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailAttempts",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailAttempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Users");
        }
    }
}
