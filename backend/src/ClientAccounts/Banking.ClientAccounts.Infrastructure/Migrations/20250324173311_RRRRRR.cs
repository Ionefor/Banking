using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.ClientAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RRRRRR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "account_id",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "user_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_account_id",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "account_id");
        }
    }
}
