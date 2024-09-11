using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleTopicId = table.Column<long>(type: "bigint", nullable: true),
                    FacilitatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    TrainingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTables_AspNetUsers_FacilitatorId",
                        column: x => x.FacilitatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeTables_ModuleTopics_ModuleTopicId",
                        column: x => x.ModuleTopicId,
                        principalTable: "ModuleTopics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeTables_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_FacilitatorId",
                table: "TimeTables",
                column: "FacilitatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_ModuleTopicId",
                table: "TimeTables",
                column: "ModuleTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_TrainingId",
                table: "TimeTables",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTables");
        }
    }
}
