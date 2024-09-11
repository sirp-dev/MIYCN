using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class certificate12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateAddress",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateDate",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateLeftSideName",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateLeftSideOfficePosition",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateLeftSideOfficeTitle",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateLeftSideSignatureKey",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateLeftSideSignatureUrl",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateRightSideName",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateRightSideOfficePosition",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateRightSideOfficeTitle",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateRightSideSignatureKey",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateRightSideSignatureUrl",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateTitle",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CertificateUseLeftSidePhysicalSignature",
                table: "Trainings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CertificateUseRightSidePhysicalSignature",
                table: "Trainings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateAddress",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateDate",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateLeftSideName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateLeftSideOfficePosition",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateLeftSideOfficeTitle",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateLeftSideSignatureKey",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateLeftSideSignatureUrl",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateRightSideName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateRightSideOfficePosition",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateRightSideOfficeTitle",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateRightSideSignatureKey",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateRightSideSignatureUrl",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateTitle",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateUseLeftSidePhysicalSignature",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CertificateUseRightSidePhysicalSignature",
                table: "Trainings");
        }
    }
}
