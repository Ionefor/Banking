using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.ClientAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class R2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "account_type",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "user_account_type");

            migrationBuilder.AddColumn<Guid>(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "user_account_type",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "account_type");
        }
    }
}
