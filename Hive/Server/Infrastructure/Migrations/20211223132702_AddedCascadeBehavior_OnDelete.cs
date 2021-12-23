using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class AddedCascadeBehavior_OnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_AspNetUsers_MemberId",
                table: "OrganizationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Organizations_OrganizationId",
                table: "OrganizationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_MemberId",
                table: "ProjectUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07d20d82-1cab-442b-acc3-39b5fe6b2ae2", "e6e3a500-46ca-45e4-adcf-18d2399b05a0", "SystemAdmin", "SYSTEMADMIN" },
                    { "b46b4558-4809-460f-b2b4-1d4a75bd57c3", "8d89e80c-df35-43dd-8c84-d1e941f6d3fa", "ProjectOwner", "PROJECTOWNER" },
                    { "d87ad721-a805-4039-a3fb-6d04a879d14d", "73912d1c-da0d-4f06-96eb-c9c48800d2e9", "Contributer", "CONTRIBUTER" }
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { new Guid("31f68617-1006-4eae-9305-afed9d380c44"), new DateTime(2021, 12, 24, 18, 57, 1, 735, DateTimeKind.Local).AddTicks(3862), "Warm", 52 },
                    { new Guid("e78683ec-ddf8-4a3f-9f7f-b45d3a92e4e3"), new DateTime(2021, 12, 25, 18, 57, 1, 736, DateTimeKind.Local).AddTicks(2605), "Balmy", -2 },
                    { new Guid("2e9d324b-f644-4cbc-bb17-ee05783767cc"), new DateTime(2021, 12, 26, 18, 57, 1, 736, DateTimeKind.Local).AddTicks(2625), "Chilly", 40 },
                    { new Guid("b017e0d6-f684-471a-9901-9a32542bff14"), new DateTime(2021, 12, 27, 18, 57, 1, 736, DateTimeKind.Local).AddTicks(2628), "Scorching", -18 },
                    { new Guid("5dab107e-fa01-4707-ad42-e622a39f2057"), new DateTime(2021, 12, 28, 18, 57, 1, 736, DateTimeKind.Local).AddTicks(2630), "Mild", 23 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_AspNetUsers_MemberId",
                table: "OrganizationUsers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Organizations_OrganizationId",
                table: "OrganizationUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_MemberId",
                table: "ProjectUsers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_AspNetUsers_MemberId",
                table: "OrganizationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUsers_Organizations_OrganizationId",
                table: "OrganizationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_MemberId",
                table: "ProjectUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07d20d82-1cab-442b-acc3-39b5fe6b2ae2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b46b4558-4809-460f-b2b4-1d4a75bd57c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d87ad721-a805-4039-a3fb-6d04a879d14d");

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("2e9d324b-f644-4cbc-bb17-ee05783767cc"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("31f68617-1006-4eae-9305-afed9d380c44"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("5dab107e-fa01-4707-ad42-e622a39f2057"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("b017e0d6-f684-471a-9901-9a32542bff14"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("e78683ec-ddf8-4a3f-9f7f-b45d3a92e4e3"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_AspNetUsers_MemberId",
                table: "OrganizationUsers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUsers_Organizations_OrganizationId",
                table: "OrganizationUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_MemberId",
                table: "ProjectUsers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
