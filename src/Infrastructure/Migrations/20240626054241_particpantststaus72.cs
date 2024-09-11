using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class particpantststaus72 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilitatorTrainingStatus",
                table: "TrainingFacilitators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DescribeFacilitatorRole",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilitatorRole",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacilitatorTrainingStatus",
                table: "TrainingFacilitators");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DescribeFacilitatorRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacilitatorRole",
                table: "AspNetUsers");
        }
    }
}
