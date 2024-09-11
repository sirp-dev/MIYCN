using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class certificate129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CerificateId",
                table: "Certificates",
                newName: "CerificateNumber");

            migrationBuilder.AddColumn<int>(
                name: "CertificateStatus",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateStatus",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "CerificateNumber",
                table: "Certificates",
                newName: "CerificateId");
        }
    }
}
