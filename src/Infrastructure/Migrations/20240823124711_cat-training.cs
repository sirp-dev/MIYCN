using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cattraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TrainingCategoryId",
                table: "Trainings",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingCategoryId",
                table: "Trainings",
                column: "TrainingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingCategory_TrainingCategoryId",
                table: "Trainings",
                column: "TrainingCategoryId",
                principalTable: "TrainingCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingCategory_TrainingCategoryId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "TrainingCategory");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_TrainingCategoryId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingCategoryId",
                table: "Trainings");
        }
    }
}
