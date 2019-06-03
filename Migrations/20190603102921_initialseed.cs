using Microsoft.EntityFrameworkCore.Migrations;

namespace EMPMANA.Migrations
{
    public partial class initialseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Contact", "Department", "Email", "Name" },
                values: new object[] { 1, "8888888888", 1, "John@gmail.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Contact", "Department", "Email", "Name" },
                values: new object[] { 2, "8888888888", 1, "Vasu@gmail.com", "Vasu " });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
