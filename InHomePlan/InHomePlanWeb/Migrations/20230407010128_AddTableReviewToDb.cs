using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddTableReviewToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Review",
                newName: "Review_Date");

            migrationBuilder.AlterColumn<int>(
                name: "RegionalStaffID",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "Review_Date",
                table: "Review",
                newName: "ReviewDate");

            migrationBuilder.AlterColumn<int>(
                name: "RegionalStaffID",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Application_ApplicationID",
                table: "Review",
                column: "ApplicationID",
                principalTable: "Application",
                principalColumn: "ApplicationID");
        }
    }
}
