using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Infrastructure.Migrations
{
    public partial class seedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsAdmin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f9fcd63a-557c-49d7-8c30-dcea65f54b45"), 0, 0, "24aef6b0-811b-4ad7-af0a-29b5d9752aaf", "admin@mail.com", false, false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEE5rIqLRaEu7ln+wa3JhQ9UPGW0Ut1ziIDRU+gnL+NS361/doM1CeEjWJNg/WC1YBw==", null, false, "1b76c903-ff54-470f-b468-bbcd079b2f8b", false, "admin@mail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9fcd63a-557c-49d7-8c30-dcea65f54b45"));
        }
    }
}
