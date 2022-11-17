using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class secondupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5993498e-9a20-4d16-9842-a813ce667d51", new DateTimeOffset(new DateTime(2022, 11, 8, 11, 13, 45, 523, DateTimeKind.Unspecified).AddTicks(9892), new TimeSpan(0, 7, 0, 0, 0)), "guest@mail.com", "guest@mail.com", "AQAAAAEAACcQAAAAEB+cy6r4AdRJlWkCXI0tSOD3auBtTBfxMyk0KkuyIMFGmuNLRMEbLQ0CoTgZ4v2voQ==", "11/8/2022 11:13:45 AM +07:00" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c0e86673-bce3-4608-8c32-06763581e952"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04cafc1a-b267-4ecd-a94a-04298ac0ed5b", new DateTimeOffset(new DateTime(2022, 11, 8, 11, 7, 58, 607, DateTimeKind.Unspecified).AddTicks(3922), new TimeSpan(0, 7, 0, 0, 0)), "guest@sample-email.com", "guest@sample-email.com", "AQAAAAEAACcQAAAAEF5byhv1wvAWcN20y0aUsc305qxs48Nouk93Uz3ajDFo/pu0+KcLvBRlWGfY2SI0kw==", "11/8/2022 11:07:58 AM +07:00" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("54f16b02-a09e-42e8-8f47-d97fb54dfa94"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 8, 11, 7, 58, 607, DateTimeKind.Unspecified).AddTicks(4078), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("f2345130-92a0-4577-a79d-b4ace269ec41"),
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 11, 8, 11, 7, 58, 607, DateTimeKind.Unspecified).AddTicks(4061), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
