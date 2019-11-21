using Microsoft.EntityFrameworkCore.Migrations;

namespace PulsifiApp.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4864398-e03e-4367-9c7c-0fc79ccae626", "8b645107-3a6a-466c-8b53-aae304b1e6ed", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8567611e-2220-42cd-9c63-b37c0529fa0e", "6ba7d792-bec6-4850-a740-bfb010b6745c", "Recruiter", "RECRUITER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60bafe81-3a6f-4a77-a99a-01397d966234", "4f6da348-e1f5-4ff4-829b-b0d71bc3e23a", "Candidate", "CANDIDATE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60bafe81-3a6f-4a77-a99a-01397d966234");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8567611e-2220-42cd-9c63-b37c0529fa0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4864398-e03e-4367-9c7c-0fc79ccae626");
        }
    }
}
