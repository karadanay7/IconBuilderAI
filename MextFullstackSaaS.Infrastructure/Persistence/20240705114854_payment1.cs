using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MextFullstackSaaS.Infrastructure.Persistence
{
    /// <inheritdoc />
    public partial class payment1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "UserPayments");

            migrationBuilder.AddColumn<string>(
                name: "ConversationId",
                table: "UserPaymentHistories",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66a45ee4-d268-404e-b6d4-07d419bfd749", "AQAAAAIAAYagAAAAEFAaWbWFo4yBr56bUqk9P2nOHLWyQxgNI4k6i8Wtj44PXPCKx8WjCO+Jwp8xlvzkPQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "UserPaymentHistories");

            migrationBuilder.AddColumn<string>(
                name: "ConversationId",
                table: "UserPayments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "042bfb7e-0a9f-4832-a74f-e62ca34c4149", "AQAAAAIAAYagAAAAEKjSxw+4MmXrzf9H7JIsKwRxcg8eDQc58Wc8bJxbj8l5MOKu4mJum5iRzNAhAImb8A==" });
        }
    }
}
