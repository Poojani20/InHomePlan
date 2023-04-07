using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewTableForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_RegionalStaff_RegionalStaffID",
                table: "Review");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_RegionalStaff_RegionalStaffID",
                table: "Review",
                column: "RegionalStaffID",
                principalTable: "RegionalStaff",
                principalColumn: "RegionalStaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_RegionalStaff_RegionalStaffID",
                table: "Review");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_RegionalStaff_RegionalStaffID",
                table: "Review",
                column: "RegionalStaffID",
                principalTable: "RegionalStaff",
                principalColumn: "RegionalStaffID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
