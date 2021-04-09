using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleLibrarySystem.Migrations
{
    public partial class SLS_DB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseDate",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Books");
        }
    }
}
