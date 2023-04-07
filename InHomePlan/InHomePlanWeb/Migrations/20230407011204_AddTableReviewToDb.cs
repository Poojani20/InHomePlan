using System;
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
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review_Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Review_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Review_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationID = table.Column<int>(type: "int", nullable: false),
                    RegionalStaffID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Review_Application_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Application",
                        principalColumn: "ApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_RegionalStaff_RegionalStaffID",
                        column: x => x.RegionalStaffID,
                        principalTable: "RegionalStaff",
                        principalColumn: "RegionalStaffID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_ApplicationID",
                table: "Review",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RegionalStaffID",
                table: "Review",
                column: "RegionalStaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
