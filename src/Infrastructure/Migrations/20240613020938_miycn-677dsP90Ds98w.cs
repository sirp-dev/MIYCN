using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class miycn677dsP90Ds98w : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestion_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestion_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DialyEvaluationQuestion",
                table: "DialyEvaluationQuestion");

            migrationBuilder.RenameTable(
                name: "DialyEvaluationQuestion",
                newName: "DialyEvaluationQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_DialyEvaluationQuestion_DialyActivityId",
                table: "DialyEvaluationQuestions",
                newName: "IX_DialyEvaluationQuestions_DialyActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DialyEvaluationQuestions",
                table: "DialyEvaluationQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestions",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "DialyEvaluationQuestionId",
                principalTable: "DialyEvaluationQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DialyEvaluationQuestions",
                table: "DialyEvaluationQuestions");

            migrationBuilder.RenameTable(
                name: "DialyEvaluationQuestions",
                newName: "DialyEvaluationQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_DialyEvaluationQuestions_DialyActivityId",
                table: "DialyEvaluationQuestion",
                newName: "IX_DialyEvaluationQuestion_DialyActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DialyEvaluationQuestion",
                table: "DialyEvaluationQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyEvaluationQuestion_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestion",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_DialyUserEvaluations_DialyEvaluationQuestion_DialyEvaluationQuestionId",
                table: "DialyUserEvaluations",
                column: "DialyEvaluationQuestionId",
                principalTable: "DialyEvaluationQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
