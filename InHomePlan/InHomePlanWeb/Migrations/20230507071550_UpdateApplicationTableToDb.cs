using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileHomePlan",
                table: "Application",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileLandPlan",
                table: "Application",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileHomePlan",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "FileLandPlan",
                table: "Application");
        }
    }
}
