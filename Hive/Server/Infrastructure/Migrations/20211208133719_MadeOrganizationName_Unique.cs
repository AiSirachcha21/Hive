using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class MadeOrganizationName_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1166deb1-97ce-46ea-a2b1-25b2bdccf918");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ea2610a-a009-46e7-b0d0-26d94fb574b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1489aa8-a555-4d3a-b887-35d172ad65a1");

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("a35b1171-04b7-46cd-a6f6-a6f65fcaf665"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b337f1a4-7db0-4814-b0f9-38595c6c0a07"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b3f32019-06c8-4490-ae21-9e80964161f0"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b8492cda-6db6-4cdf-90c4-a14ab2a5af1f"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("c41a4e9f-c92e-4e04-b0ba-cdfcba47734e"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Name",
                table: "Organizations",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_Name",
                table: "Organizations");

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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c1489aa8-a555-4d3a-b887-35d172ad65a1", "af6cbcf3-2a3d-445a-8507-d18f4af518e3", "SystemAdmin", "SYSTEMADMIN" },
                    { "1166deb1-97ce-46ea-a2b1-25b2bdccf918", "da6af96a-139f-4ab4-a9ba-8bd03afbd227", "ProjectOwner", "PROJECTOWNER" },
                    { "8ea2610a-a009-46e7-b0d0-26d94fb574b0", "3642ed42-6d1f-4307-83fe-92b9d406d113", "Contributer", "CONTRIBUTER" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("b337f1a4-7db0-4814-b0f9-38595c6c0a07"), new DateTime(2021, 12, 4, 20, 47, 33, 588, DateTimeKind.Local).AddTicks(4667), "Sweltering", 15 },
                    { new Guid("b3f32019-06c8-4490-ae21-9e80964161f0"), new DateTime(2021, 12, 5, 20, 47, 33, 589, DateTimeKind.Local).AddTicks(67), "Bracing", -18 },
                    { new Guid("b8492cda-6db6-4cdf-90c4-a14ab2a5af1f"), new DateTime(2021, 12, 6, 20, 47, 33, 589, DateTimeKind.Local).AddTicks(86), "Mild", -17 },
                    { new Guid("a35b1171-04b7-46cd-a6f6-a6f65fcaf665"), new DateTime(2021, 12, 7, 20, 47, 33, 589, DateTimeKind.Local).AddTicks(89), "Sweltering", 31 },
                    { new Guid("c41a4e9f-c92e-4e04-b0ba-cdfcba47734e"), new DateTime(2021, 12, 8, 20, 47, 33, 589, DateTimeKind.Local).AddTicks(92), "Chilly", 39 }
                });
        }
    }
}
