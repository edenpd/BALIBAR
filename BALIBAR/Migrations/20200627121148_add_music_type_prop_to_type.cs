using Microsoft.EntityFrameworkCore.Migrations;

namespace BALIBAR.Migrations
{
    public partial class add_music_type_prop_to_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MusicType",
                table: "Type",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicType",
                table: "Type");
        }
    }
}
