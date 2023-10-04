using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopcornBackend.Migrations
{
    /// <inheritdoc />
    public partial class addedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "U");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "U",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "User");
        }
    }
}
