using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class itocert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CerificateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateType = table.Column<int>(type: "int", nullable: false),
                    IssuerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificates_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateAttendanceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftOffSignature = table.Column<bool>(type: "bit", nullable: false),
                    LeftSignatureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftSignatureKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftTitleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightOffSignature = table.Column<bool>(type: "bit", nullable: false),
                    RightSignatureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightSignatureKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightTitleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_TrainingId",
                table: "Certificates",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserId",
                table: "Certificates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_TrainingId",
                table: "Settings",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
