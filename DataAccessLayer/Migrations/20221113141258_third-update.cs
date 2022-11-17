using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class thirdupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AspNetUsers",
                newName: "JoinDate");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "Address", "ConcurrencyStamp", "Email", "FirstName", "JoinDate", "LastName", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "Ho Chi Minh, Viet Nam", "41c35264-2364-4a3b-a693-0264d5a54301", "guest@mail.com", "A ", new DateTimeOffset(new DateTime(2022, 11, 13, 21, 12, 58, 253, DateTimeKind.Unspecified).AddTicks(7827), new TimeSpan(0, 7, 0, 0, 0)), "Van Pham", "AQAAAAEAACcQAAAAEJK5z08a3xxjX3mBedy/h3YUrxAhSrqTVHN+xHQ9gv17rPNGErVpifEjCUUkhyag8A==", "0785686004", "11/13/2022 9:12:58 PM +07:00" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "AspNetUsers",
                newName: "CreatedDate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "Email", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "5993498e-9a20-4d16-9842-a813ce667d51", new DateTimeOffset(new DateTime(2022, 11, 8, 11, 13, 45, 523, DateTimeKind.Unspecified).AddTicks(9892), new TimeSpan(0, 7, 0, 0, 0)), "guest1@mail.com", "AQAAAAEAACcQAAAAEB+cy6r4AdRJlWkCXI0tSOD3auBtTBfxMyk0KkuyIMFGmuNLRMEbLQ0CoTgZ4v2voQ==", null, "11/8/2022 11:13:45 AM +07:00" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("54f16b02-a09e-42e8-8f47-d97fb54dfa94"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 8, 11, 13, 45, 524, DateTimeKind.Unspecified).AddTicks(14), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("f2345130-92a0-4577-a79d-b4ace269ec41"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 8, 11, 13, 45, 523, DateTimeKind.Unspecified).AddTicks(9999), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
