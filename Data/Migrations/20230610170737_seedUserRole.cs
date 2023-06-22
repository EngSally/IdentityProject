using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Data.Migrations
{
    public partial class seedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7abbffb1-5a1d-4cdb-9038-6634ef561808", "c8c6af02-ba8c-4fc1-8639-5de7db75d649" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8cfd609-d8d6-4be7-b86e-ef94c03461b6", "33d67ecd-5059-4efe-9fc6-7c1dc87a6879" });
        }
    }
}
