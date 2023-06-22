using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Data.Migrations
{
    public partial class seedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8cfd609-d8d6-4be7-b86e-ef94c03461b6", "33d67ecd-5059-4efe-9fc6-7c1dc87a6879" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0e8e108d-f366-45ec-ba53-40cada65b1d2", "cdbe7784-0d3e-441a-9c48-a317fff87e71" });
        }
    }
}
