using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.ClientAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WWW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_client_accounts_client_accounts_id",
                schema: "ClientAccounts",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_cards_client_accounts_client_accounts_id",
                schema: "ClientAccounts",
                table: "cards");

            migrationBuilder.RenameColumn(
                name: "account_id",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "user_account_id");

            migrationBuilder.RenameColumn(
                name: "client_accounts_id",
                schema: "ClientAccounts",
                table: "cards",
                newName: "client_account_id");

            migrationBuilder.RenameIndex(
                name: "ix_cards_client_accounts_id",
                schema: "ClientAccounts",
                table: "cards",
                newName: "ix_cards_client_account_id");

            migrationBuilder.RenameColumn(
                name: "client_accounts_id",
                schema: "ClientAccounts",
                table: "accounts",
                newName: "client_account_id");

            migrationBuilder.RenameIndex(
                name: "ix_accounts_client_accounts_id",
                schema: "ClientAccounts",
                table: "accounts",
                newName: "ix_accounts_client_account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_client_accounts_client_account_id",
                schema: "ClientAccounts",
                table: "accounts",
                column: "client_account_id",
                principalSchema: "ClientAccounts",
                principalTable: "client_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cards_client_accounts_client_account_id",
                schema: "ClientAccounts",
                table: "cards",
                column: "client_account_id",
                principalSchema: "ClientAccounts",
                principalTable: "client_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_client_accounts_client_account_id",
                schema: "ClientAccounts",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_cards_client_accounts_client_account_id",
                schema: "ClientAccounts",
                table: "cards");

            migrationBuilder.RenameColumn(
                name: "user_account_id",
                schema: "ClientAccounts",
                table: "client_accounts",
                newName: "account_id");

            migrationBuilder.RenameColumn(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "cards",
                newName: "client_accounts_id");

            migrationBuilder.RenameIndex(
                name: "ix_cards_client_account_id",
                schema: "ClientAccounts",
                table: "cards",
                newName: "ix_cards_client_accounts_id");

            migrationBuilder.RenameColumn(
                name: "client_account_id",
                schema: "ClientAccounts",
                table: "accounts",
                newName: "client_accounts_id");

            migrationBuilder.RenameIndex(
                name: "ix_accounts_client_account_id",
                schema: "ClientAccounts",
                table: "accounts",
                newName: "ix_accounts_client_accounts_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_client_accounts_client_accounts_id",
                schema: "ClientAccounts",
                table: "accounts",
                column: "client_accounts_id",
                principalSchema: "ClientAccounts",
                principalTable: "client_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cards_client_accounts_client_accounts_id",
                schema: "ClientAccounts",
                table: "cards",
                column: "client_accounts_id",
                principalSchema: "ClientAccounts",
                principalTable: "client_accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
