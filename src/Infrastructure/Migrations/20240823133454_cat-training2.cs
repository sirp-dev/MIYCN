using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cattraining2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingCategory_TrainingCategoryId",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingCategory",
                table: "TrainingCategory");

            migrationBuilder.RenameTable(
                name: "TrainingCategory",
                newName: "TrainingCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingCategories",
                table: "TrainingCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingCategories_TrainingCategoryId",
                table: "Trainings",
                column: "TrainingCategoryId",
                principalTable: "TrainingCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingCategories_TrainingCategoryId",
                table: "Trainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingCategories",
                table: "TrainingCategories");

            migrationBuilder.RenameTable(
                name: "TrainingCategories",
                newName: "TrainingCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingCategory",
                table: "TrainingCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingCategory_TrainingCategoryId",
                table: "Trainings",
                column: "TrainingCategoryId",
                principalTable: "TrainingCategory",
                principalColumn: "Id");
        }
    }
}
