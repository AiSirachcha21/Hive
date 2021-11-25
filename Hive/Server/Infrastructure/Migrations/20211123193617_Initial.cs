using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperatureC = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("8d4d73f1-71ab-42a7-bb58-769e7b332595"), new DateTime(2021, 11, 25, 1, 6, 16, 947, DateTimeKind.Local).AddTicks(9877), "Scorching", 29 },
                    { new Guid("ac5677a9-ac8e-48dc-ab8c-06985b8b0458"), new DateTime(2021, 11, 26, 1, 6, 16, 949, DateTimeKind.Local).AddTicks(2818), "Balmy", 10 },
                    { new Guid("122ba593-dd82-4061-b664-1f40c91dd856"), new DateTime(2021, 11, 27, 1, 6, 16, 949, DateTimeKind.Local).AddTicks(2868), "Bracing", 38 },
                    { new Guid("7c0766fa-3223-40a9-b7f0-ef4cabb9b9d3"), new DateTime(2021, 11, 28, 1, 6, 16, 949, DateTimeKind.Local).AddTicks(2872), "Bracing", 37 },
                    { new Guid("86b1ced5-8506-4f71-8c2d-297faddb62f4"), new DateTime(2021, 11, 29, 1, 6, 16, 949, DateTimeKind.Local).AddTicks(2876), "Mild", -11 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");
        }
    }
}
