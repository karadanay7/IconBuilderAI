using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence
{
    /// <inheritdoc />
    public partial class mig_userprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "ImageUrl", "PasswordHash" },
                values: new object[] { "e1bfd58d-218e-4a5f-a3b5-277bf476b859", null, "AQAAAAIAAYagAAAAEJjxYw0djATDG+iHZKc4bQ2GAV5RRzjm7dDIM5v8FLOSjKgypB9wvA5nAn6ZIBZ+Eg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0fc3118e-bd2d-4045-8efe-7dbac111e042", "AQAAAAIAAYagAAAAEOV0S0vjJkKOZT/K8FnVAVixCq05+O5B5qP+uAqWGEYSeBQ7mXRSO3sBXQj/pNpvOA==" });
        }
    }
}
