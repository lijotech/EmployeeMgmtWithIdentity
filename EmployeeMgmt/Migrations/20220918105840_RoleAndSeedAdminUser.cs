using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMgmt.Migrations
{
    public partial class RoleAndSeedAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16581259-fc9c-4436-8a2a-f4a8ed2a8d92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36ec24ed-29bc-4787-ac4b-424b06fb1001");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0354acd-9219-4621-ba32-282db259c081", "79936d09-4a72-4d7c-ba00-215bea9c3fc3", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ccce14b5-123c-4166-981c-21e23aaef4c2", "cae8c658-f15d-4bb1-865a-4732023af8c8", "Employee", "Employee" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "330ff8fa-a471-4bf7-8011-21bab7a7a23b", 0, "2ba76095-4478-4f90-a4ae-792a52016fdc", "admin@admincontrol.com", false, false, null, null, "admin@admincontrol.com", "AQAAAAEAACcQAAAAEM/NjwfXDecizSl2m+X1i5/EwEoslbHI1znUZCr1oPfNyxW9II5BwnuysqtnldCatg==", null, false, "9f73144e-252c-46a5-8dc9-9c3df61264bd", false, "admin@admincontrol.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b0354acd-9219-4621-ba32-282db259c081", "330ff8fa-a471-4bf7-8011-21bab7a7a23b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccce14b5-123c-4166-981c-21e23aaef4c2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b0354acd-9219-4621-ba32-282db259c081", "330ff8fa-a471-4bf7-8011-21bab7a7a23b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0354acd-9219-4621-ba32-282db259c081");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "330ff8fa-a471-4bf7-8011-21bab7a7a23b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16581259-fc9c-4436-8a2a-f4a8ed2a8d92", "a577853d-58da-459d-9201-98fef069316f", "Employee", "Employee" },
                    { "36ec24ed-29bc-4787-ac4b-424b06fb1001", "7d2f0361-2300-4171-af18-e8f4c74cf1da", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email" },
                values: new object[,]
                {
                    { 1, 1, "mary@gmail.com" },
                    { 2, 2, "john@gmail.com" },
                    { 3, 2, "smith@gmail.com" },
                    { 4, 3, "robert@gmail.com" },
                    { 5, 3, "oliver@gmail.com" },
                    { 6, 2, "ann@gmail.com" },
                    { 7, 2, "kriz@gmail.com" }
                });
        }
    }
}
