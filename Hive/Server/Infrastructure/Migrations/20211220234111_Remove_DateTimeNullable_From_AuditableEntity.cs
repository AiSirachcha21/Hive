using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class Remove_DateTimeNullable_From_AuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eba9784-aa7a-41db-b7df-c8bbbd67d58a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45d3a241-5e96-47ff-83c2-8a4ea8fad3e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48814509-4cf2-4bf1-8398-86738739a667");

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("0bbe88bc-6273-4def-8b11-27bf9599d145"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("1088b833-d9a4-4854-9a91-24b5c79cd6c2"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("1dd85420-500b-4b23-881d-caf8d60414dd"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("7cf98976-f1f4-425a-b429-079da1f9d521"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("cbb223aa-aa30-488a-80ff-c917bdbf311b"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModfied",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModfied",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ac4d4c4a-09ff-404d-8ca9-189bde48234b", "8a20a2cd-9c25-4f49-bb26-528827fb38cd", "SystemAdmin", "SYSTEMADMIN" },
                    { "1ee02757-c75a-490c-a670-9f2e70f69a10", "ee73e09d-1eba-4283-90d9-27667493898e", "ProjectOwner", "PROJECTOWNER" },
                    { "81708089-cd82-4955-b9cc-ba9d38c2b67a", "ad5644dd-7890-46bf-a9a7-1e383458444f", "Contributer", "CONTRIBUTER" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("78d70280-ca37-4cf6-8180-d7da181ee947"), new DateTime(2021, 12, 22, 5, 11, 10, 695, DateTimeKind.Local).AddTicks(6113), "Mild", -18 },
                    { new Guid("71c2e54b-8ca3-4847-a2b3-3b1b3a6aca1d"), new DateTime(2021, 12, 23, 5, 11, 10, 696, DateTimeKind.Local).AddTicks(5257), "Scorching", 20 },
                    { new Guid("57312891-0050-4f20-89a0-6ec44e8c87b3"), new DateTime(2021, 12, 24, 5, 11, 10, 696, DateTimeKind.Local).AddTicks(5278), "Balmy", -10 },
                    { new Guid("28f8e76f-d9c7-449f-91be-47f3034ae59d"), new DateTime(2021, 12, 25, 5, 11, 10, 696, DateTimeKind.Local).AddTicks(5280), "Chilly", 36 },
                    { new Guid("514d6d10-e315-45e8-96da-5876154d04aa"), new DateTime(2021, 12, 26, 5, 11, 10, 696, DateTimeKind.Local).AddTicks(5283), "Bracing", 41 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ee02757-c75a-490c-a670-9f2e70f69a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81708089-cd82-4955-b9cc-ba9d38c2b67a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac4d4c4a-09ff-404d-8ca9-189bde48234b");

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("28f8e76f-d9c7-449f-91be-47f3034ae59d"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("514d6d10-e315-45e8-96da-5876154d04aa"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("57312891-0050-4f20-89a0-6ec44e8c87b3"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("71c2e54b-8ca3-4847-a2b3-3b1b3a6aca1d"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("78d70280-ca37-4cf6-8180-d7da181ee947"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModfied",
                table: "Tickets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModfied",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eba9784-aa7a-41db-b7df-c8bbbd67d58a", "a4bfcf17-d9cf-416f-8f45-9522a6b1d7b0", "SystemAdmin", "SYSTEMADMIN" },
                    { "45d3a241-5e96-47ff-83c2-8a4ea8fad3e6", "232554f9-121d-4100-a717-18c515fb0dcd", "ProjectOwner", "PROJECTOWNER" },
                    { "48814509-4cf2-4bf1-8398-86738739a667", "58e70de7-dd95-4d1f-bf4a-126df1facb56", "Contributer", "CONTRIBUTER" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("0bbe88bc-6273-4def-8b11-27bf9599d145"), new DateTime(2021, 12, 9, 19, 7, 18, 740, DateTimeKind.Local).AddTicks(3517), "Freezing", -14 },
                    { new Guid("1088b833-d9a4-4854-9a91-24b5c79cd6c2"), new DateTime(2021, 12, 10, 19, 7, 18, 741, DateTimeKind.Local).AddTicks(1599), "Hot", 8 },
                    { new Guid("cbb223aa-aa30-488a-80ff-c917bdbf311b"), new DateTime(2021, 12, 11, 19, 7, 18, 741, DateTimeKind.Local).AddTicks(1617), "Cool", 54 },
                    { new Guid("1dd85420-500b-4b23-881d-caf8d60414dd"), new DateTime(2021, 12, 12, 19, 7, 18, 741, DateTimeKind.Local).AddTicks(1619), "Chilly", 23 },
                    { new Guid("7cf98976-f1f4-425a-b429-079da1f9d521"), new DateTime(2021, 12, 13, 19, 7, 18, 741, DateTimeKind.Local).AddTicks(1622), "Chilly", -5 }
                });
        }
    }
}
