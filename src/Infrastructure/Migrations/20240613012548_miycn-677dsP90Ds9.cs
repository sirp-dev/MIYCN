using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class miycn677dsP90Ds9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.RenameColumn(
                name: "EvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "DialyEvaluationQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DialyUserEvaluations_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "IX_DialyUserEvaluations_DialyEvaluationQuestionId");

            migrationBuilder.CreateTable(
                name: "DialyEvaluationQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsModuleTopic = table.Column<bool>(type: "bit", nullable: false),
                    EvaluationAnswerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyEvaluationQuestion", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestion_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "DialyEvaluationQuestionId",
                principalTable: "DialyEvaluationQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestion_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropTable(
                name: "DialyEvaluationQuestion");

            migrationBuilder.RenameColumn(
                name: "DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "EvaluationQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DialyUserEvaluations_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                newName: "IX_DialyUserEvaluations_EvaluationQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_EvaluationQuestions_EvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "EvaluationQuestionId",
                principalTable: "EvaluationQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
