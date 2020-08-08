using Microsoft.EntityFrameworkCore.Migrations;

namespace BALIBAR.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Bar_BarId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "BarId",
                table: "Reservation",
                newName: "BarID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_BarId",
                table: "Reservation",
                newName: "IX_Reservation_BarID");

            migrationBuilder.AlterColumn<int>(
                name: "BarID",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Bar_BarID",
                table: "Reservation",
                column: "BarID",
                principalTable: "Bar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Bar_BarID",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "BarID",
                table: "Reservation",
                newName: "BarId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_BarID",
                table: "Reservation",
                newName: "IX_Reservation_BarId");

            migrationBuilder.AlterColumn<int>(
                name: "BarId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Bar_BarId",
                table: "Reservation",
                column: "BarId",
                principalTable: "Bar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
