using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PulsifiApp.Data.Migrations
{
    public partial class AddJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");
        }
    }
}
