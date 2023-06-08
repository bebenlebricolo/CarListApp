using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarListApi.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d2f2532-68a4-4e01-89eb-00e6cf54138b", null, "User", "USER" },
                    { "2304b13a-86e4-4c5c-8325-f8acb266ab34", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4", 0, "0921b618-a9fc-4bd5-8686-84f9e9ae1d17", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAECofYBB1PSzykQk+NUi4ktKnwdeXaDpY2w21pBrfaMaOChE2ZFbeqjnEk3nj2+vmZw==", null, false, "1950c7f1-1d22-4df7-b279-fa40f777bb8b", false, null },
                    { "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0", 0, "7718684c-5ec3-46a8-b0c7-707257c658e0", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEGbgtAonMDkYygcpTcyI2aCtSvSFmFLQja9XtBMY8AdQSFdDZX/EqSmWsMc75JftuA==", null, false, "bfa60142-7c8c-46e3-a7bb-029dee199faa", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2304b13a-86e4-4c5c-8325-f8acb266ab34", "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4" },
                    { "0d2f2532-68a4-4e01-89eb-00e6cf54138b", "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2304b13a-86e4-4c5c-8325-f8acb266ab34", "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0d2f2532-68a4-4e01-89eb-00e6cf54138b", "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d2f2532-68a4-4e01-89eb-00e6cf54138b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2304b13a-86e4-4c5c-8325-f8acb266ab34");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0bb7fcc-58c0-4b68-a761-6a257c3b8ba4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da62ea4f-d884-4b67-87f3-ed0dbfdbf1c0");
        }
    }
}
