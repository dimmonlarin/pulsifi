using Microsoft.EntityFrameworkCore.Migrations;

namespace PulsifiApp.Data.Migrations
{
    public partial class AddLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Description" },
                values: new object[] { 1, "Everywhere" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Description" },
                values: new object[] { 2, "Kuala Lumpur" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Description" },
                values: new object[] { 3, "Ipoh" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_LocationID",
                table: "Jobs",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Locations_LocationID",
                table: "Jobs",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Locations_LocationID",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_LocationID",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Job",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "ID");
        }
    }
}
