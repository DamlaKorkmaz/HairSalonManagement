using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalonManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeServices",
                schema: "public",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "integer", nullable: false),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeServices", x => new { x.EmployeeID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_EmployeeServices_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "public",
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeServices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalSchema: "public",
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeServices_ServiceID",
                schema: "public",
                table: "EmployeeServices",
                column: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeServices",
                schema: "public");
        }
    }
}
