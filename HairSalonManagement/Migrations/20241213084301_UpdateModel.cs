using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalonManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Salons_SalonID",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeID",
                table: "Appointments",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Salons_SalonID",
                table: "Appointments",
                column: "SalonID",
                principalTable: "Salons",
                principalColumn: "SalonID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Salons_SalonID",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeID",
                table: "Appointments",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Salons_SalonID",
                table: "Appointments",
                column: "SalonID",
                principalTable: "Salons",
                principalColumn: "SalonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
