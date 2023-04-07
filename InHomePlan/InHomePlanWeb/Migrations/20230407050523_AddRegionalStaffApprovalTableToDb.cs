using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InHomePlanWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionalStaffApprovalTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regional_Staff_Approval",
                columns: table => new
                {
                    RegionalStaffApprovalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalID = table.Column<int>(type: "int", nullable: false),
                    RegionalStaffID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regional_Staff_Approval", x => x.RegionalStaffApprovalId);
                    table.ForeignKey(
                        name: "FK_Regional_Staff_Approval_Approval_ApprovalID",
                        column: x => x.ApprovalID,
                        principalTable: "Approval",
                        principalColumn: "ApprovalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Regional_Staff_Approval_RegionalStaff_RegionalStaffID",
                        column: x => x.RegionalStaffID,
                        principalTable: "RegionalStaff",
                        principalColumn: "RegionalStaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regional_Staff_Approval_ApprovalID",
                table: "Regional_Staff_Approval",
                column: "ApprovalID");

            migrationBuilder.CreateIndex(
                name: "IX_Regional_Staff_Approval_RegionalStaffID",
                table: "Regional_Staff_Approval",
                column: "RegionalStaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Regional_Staff_Approval");
        }
    }
}
