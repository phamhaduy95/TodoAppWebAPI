using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class forthupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Organization",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "ConcurrencyStamp", "DisplayName", "JoinDate", "Organization", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12a6f019-0eef-4d97-8a69-a9c259693279", "Guest", new DateTimeOffset(new DateTime(2022, 11, 15, 1, 45, 5, 486, DateTimeKind.Unspecified).AddTicks(6214), new TimeSpan(0, 7, 0, 0, 0)), "PHD inc", "AQAAAAEAACcQAAAAEOFQf3xtjkODe6NtSX54EQM56VzhLpHsSjZP+wJ/q00l7Lu4cnjPkxq9xnM/USghWA==", "11/15/2022 1:45:05 AM +07:00" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("54f16b02-a09e-42e8-8f47-d97fb54dfa94"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 15, 1, 45, 5, 486, DateTimeKind.Unspecified).AddTicks(6356), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("f2345130-92a0-4577-a79d-b4ace269ec41"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 15, 1, 45, 5, 486, DateTimeKind.Unspecified).AddTicks(6340), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Organization",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "Organization", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41c35264-2364-4a3b-a693-0264d5a54301", new DateTimeOffset(new DateTime(2022, 11, 13, 21, 12, 58, 253, DateTimeKind.Unspecified).AddTicks(7827), new TimeSpan(0, 7, 0, 0, 0)), null, "AQAAAAEAACcQAAAAEJK5z08a3xxjX3mBedy/h3YUrxAhSrqTVHN+xHQ9gv17rPNGErVpifEjCUUkhyag8A==", "11/13/2022 9:12:58 PM +07:00" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("54f16b02-a09e-42e8-8f47-d97fb54dfa94"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 13, 21, 12, 58, 253, DateTimeKind.Unspecified).AddTicks(7939), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("f2345130-92a0-4577-a79d-b4ace269ec41"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 13, 21, 12, 58, 253, DateTimeKind.Unspecified).AddTicks(7923), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
