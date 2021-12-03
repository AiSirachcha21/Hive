using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class AddEntityModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemAdminId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_AspNetUsers_SystemAdminId",
                        column: x => x.SystemAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModfied = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_ProjectOwnerId",
                        column: x => x.ProjectOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModfied = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_SystemAdminId",
                table: "Organizations",
                column: "SystemAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_MemberId",
                table: "OrganizationUsers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_OrganizationId",
                table: "OrganizationUsers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectOwnerId",
                table: "Projects",
                column: "ProjectOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_MemberId",
                table: "ProjectUsers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedUserId",
                table: "Tickets",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickets",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationUsers");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Organizations");

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

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

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
    }
}
