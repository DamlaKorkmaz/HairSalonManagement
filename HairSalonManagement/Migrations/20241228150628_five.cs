using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalonManagement.Migrations
{
    /// <inheritdoc />
    public partial class five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Uzmanlar_UzmanId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_UzmanId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "UzmanId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "Tarih",
                table: "Randevular",
                newName: "RandevuTarihi");

            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Soyisim",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UzmanIsim",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isim",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Soyisim",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "UzmanIsim",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "RandevuTarihi",
                table: "Randevular",
                newName: "Tarih");

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UzmanId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_UzmanId",
                table: "Randevular",
                column: "UzmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Uzmanlar_UzmanId",
                table: "Randevular",
                column: "UzmanId",
                principalTable: "Uzmanlar",
                principalColumn: "UzmanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
