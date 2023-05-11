using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationID",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Status_ApplicationID",
                table: "Status",
                column: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Application_ApplicationID",
                table: "Status",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Status_Application_ApplicationID",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Status_ApplicationID",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "ApplicationID",
                table: "Status");
        }
    }
}
