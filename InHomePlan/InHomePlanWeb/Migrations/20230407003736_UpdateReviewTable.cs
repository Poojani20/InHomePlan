using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
