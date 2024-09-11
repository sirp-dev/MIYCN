using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class miycn677ds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId1",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_UserId1",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "UserTests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostTestInstruction",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreTestInstruction",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId",
                table: "UserTests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId",
                table: "UserTests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_UserId",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "PostTestInstruction",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "PreTestInstruction",
                table: "Trainings");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserTests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId1",
                table: "UserTests",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_UserId1",
                table: "UserTests",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
