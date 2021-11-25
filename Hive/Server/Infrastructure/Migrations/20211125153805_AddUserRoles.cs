using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("aa977393-7deb-404d-83e2-01dba0ecfa98"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("ad75597e-8a21-4f10-ae94-df4cc3b2237d"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b8204d16-3e84-44ee-8c66-86c48579d06a"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("c8247c93-1739-499b-9f4e-0d4625b0207a"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("e06a254f-2a37-4db8-9b95-a01bbb29d9f2"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "faafc617-85e8-473f-b4fd-f232d51bb9e6", "2f3043ac-69fc-4fa6-8195-fea3423d02d5", "SystemAdmin", "SYSTEMADMIN" },
                    { "2e8ec6bd-8440-4e36-a935-f9b1384b3570", "a9854e67-4f72-4ccd-bf27-44deb764a03f", "ProjectOwner", "PROJECTOWNER" },
                    { "405c59c3-cec0-4c4b-afe2-b4ab529acaf0", "a4f46039-d36b-41f5-ace9-d9328b4357e3", "Contributer", "CONTRIBUTER" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("39ab77d2-6ea2-49d6-b5a4-4104c1f78192"), new DateTime(2021, 11, 26, 21, 8, 5, 202, DateTimeKind.Local).AddTicks(1854), "Cool", 11 },
                    { new Guid("b17c18a6-e5e9-41eb-8f42-ffefba7e9a56"), new DateTime(2021, 11, 27, 21, 8, 5, 207, DateTimeKind.Local).AddTicks(5433), "Scorching", 31 },
                    { new Guid("e14bedbe-8efa-4c97-8e4c-0732d913b7db"), new DateTime(2021, 11, 28, 21, 8, 5, 207, DateTimeKind.Local).AddTicks(5468), "Sweltering", 32 },
                    { new Guid("dd64980c-8e20-4a71-bbd8-c8d54959b708"), new DateTime(2021, 11, 29, 21, 8, 5, 207, DateTimeKind.Local).AddTicks(5472), "Hot", 26 },
                    { new Guid("575a2caa-8fc0-493d-8617-bf2167c10efb"), new DateTime(2021, 11, 30, 21, 8, 5, 207, DateTimeKind.Local).AddTicks(5475), "Sweltering", 33 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e8ec6bd-8440-4e36-a935-f9b1384b3570");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "405c59c3-cec0-4c4b-afe2-b4ab529acaf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faafc617-85e8-473f-b4fd-f232d51bb9e6");

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("39ab77d2-6ea2-49d6-b5a4-4104c1f78192"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("575a2caa-8fc0-493d-8617-bf2167c10efb"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b17c18a6-e5e9-41eb-8f42-ffefba7e9a56"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("dd64980c-8e20-4a71-bbd8-c8d54959b708"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("e14bedbe-8efa-4c97-8e4c-0732d913b7db"));

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("c8247c93-1739-499b-9f4e-0d4625b0207a"), new DateTime(2021, 11, 25, 2, 4, 54, 524, DateTimeKind.Local).AddTicks(1960), "Mild", 36 },
                    { new Guid("aa977393-7deb-404d-83e2-01dba0ecfa98"), new DateTime(2021, 11, 26, 2, 4, 54, 525, DateTimeKind.Local).AddTicks(8727), "Hot", 50 },
                    { new Guid("b8204d16-3e84-44ee-8c66-86c48579d06a"), new DateTime(2021, 11, 27, 2, 4, 54, 525, DateTimeKind.Local).AddTicks(8802), "Freezing", 36 },
                    { new Guid("e06a254f-2a37-4db8-9b95-a01bbb29d9f2"), new DateTime(2021, 11, 28, 2, 4, 54, 525, DateTimeKind.Local).AddTicks(8811), "Mild", -15 },
                    { new Guid("ad75597e-8a21-4f10-ae94-df4cc3b2237d"), new DateTime(2021, 11, 29, 2, 4, 54, 525, DateTimeKind.Local).AddTicks(8820), "Cool", -16 }
                });
        }
    }
}
