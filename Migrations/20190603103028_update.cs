using Microsoft.EntityFrameworkCore.Migrations;

namespace EMPMANA.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Department", "Email", "Name" },
                values: new object[] { 0, "Andy@gmail.com", "Andy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Department", "Email", "Name" },
                values: new object[] { 1, "John@gmail.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Contact", "Department", "Email", "Name" },
                values: new object[] { 2, "8888888888", 1, "Vasu@gmail.com", "Vasu " });
        }
    }
}
