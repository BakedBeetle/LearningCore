using Microsoft.EntityFrameworkCore.Migrations;

namespace EMPMANA.Migrations
{
    public partial class insertupdte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Doey@gmail.com", "John" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Contact", "Department", "Email", "Name" },
                values: new object[] { 2, "8888888888", 0, "Vasu@gmail.com", "Vasu" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Andy@gmail.com", "Andy" });
        }
    }
}
