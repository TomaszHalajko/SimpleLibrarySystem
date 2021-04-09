using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleLibrarySystem.Migrations
{
    public partial class SLS_DB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Vorname = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "ISBN", "Name", "Publisher", "ReleaseDate" },
                values: new object[,]
                {
                    { "2013-000001", "Sarah J. Mass", "9788377478844", "Szklany tron", "Uroboros", 2013 },
                    { "2012-000001", "J.R.R. Tolkien", "9788324144242", "Drużyna Pierścienia", "Amber", 2012 },
                    { "2014-000001", "Andrzej Sapkowski", "9788375780635", "Ostatnie życzenie", "superNowa", 2014 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "Email", "Name", "PhoneNumber", "Vorname" },
                values: new object[] { "0000001", "Jelenia Góra, ul.Kadetów 1", "halajko.tomasz@gmail.com", "Hałajko", "883791001", "Tomasz" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: "2012-000001");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: "2013-000001");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: "2014-000001");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");
        }
    }
}
