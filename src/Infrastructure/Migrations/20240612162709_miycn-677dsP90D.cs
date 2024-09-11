using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class miycn677dsP90D : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EvaluationQuestionCategoryId",
                table: "EvaluationQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestions_EvaluationQuestionCategoryId",
                table: "EvaluationQuestions",
                column: "EvaluationQuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationQuestions_EvaluationQuestionCategories_EvaluationQuestionCategoryId",
                table: "EvaluationQuestions",
                column: "EvaluationQuestionCategoryId",
                principalTable: "EvaluationQuestionCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationQuestions_EvaluationQuestionCategories_EvaluationQuestionCategoryId",
                table: "EvaluationQuestions");

            migrationBuilder.DropTable(
                name: "EvaluationQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationQuestions_EvaluationQuestionCategoryId",
                table: "EvaluationQuestions");

            migrationBuilder.DropColumn(
                name: "EvaluationQuestionCategoryId",
                table: "EvaluationQuestions");
        }
    }
}
