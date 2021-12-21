using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hive.Server.Infrastructure.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("122ba593-dd82-4061-b664-1f40c91dd856"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("7c0766fa-3223-40a9-b7f0-ef4cabb9b9d3"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("86b1ced5-8506-4f71-8c2d-297faddb62f4"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("8d4d73f1-71ab-42a7-bb58-769e7b332595"));

            migrationBuilder.DeleteData(
                table: "Forecasts",
                keyColumn: "Id",
                keyValue: new Guid("ac5677a9-ac8e-48dc-ab8c-06985b8b0458"));

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
    }
}
