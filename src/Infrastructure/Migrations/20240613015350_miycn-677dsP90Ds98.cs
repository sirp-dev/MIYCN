using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class miycn677dsP90Ds98 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DialyEvaluationQuestion_DialyActivityId",
                table: "DialyEvaluationQuestion",
                column: "DialyActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyEvaluationQuestion_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestion",
                column: "DialyActivityId",
                principalTable: "DialyActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyEvaluationQuestion_DialyActivities_DialyActivityId",
                table: "DialyEvaluationQuestion");

            migrationBuilder.DropIndex(
                name: "IX_DialyEvaluationQuestion_DialyActivityId",
                table: "DialyEvaluationQuestion");

            migrationBuilder.DropColumn(
                name: "DialyActivityId",
                table: "DialyEvaluationQuestion");
        }
    }
}
