using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsignin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttendanceStatus",
                table: "Attendances",
                newName: "AttendanceSignOutStatus");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceSignInStatus",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceSignInStatus",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "AttendanceSignOutStatus",
                table: "Attendances",
                newName: "AttendanceStatus");
        }
    }
}
