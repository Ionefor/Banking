using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.ClientAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_W : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ClientAccounts");

            migrationBuilder.CreateTable(
                name: "client_accounts",
                schema: "ClientAccounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_account_type = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "ClientAccounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    сurrency = table.Column<int>(type: "integer", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<double>(type: "double precision", maxLength: 20, nullable: false),
                    payment_details = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounts_client_accounts_account_id",
                        column: x => x.account_id,
                        principalSchema: "ClientAccounts",
                        principalTable: "client_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                schema: "ClientAccounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    validThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    card_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ccv = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    payment_details = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_cards_accounts_account_id",
                        column: x => x.account_id,
                        principalSchema: "ClientAccounts",
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cards_client_accounts_card_id",
                        column: x => x.card_id,
                        principalSchema: "ClientAccounts",
                        principalTable: "client_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accounts_account_id",
                schema: "ClientAccounts",
                table: "accounts",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_cards_account_id",
                schema: "ClientAccounts",
                table: "cards",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_cards_card_id",
                schema: "ClientAccounts",
                table: "cards",
                column: "card_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards",
                schema: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "accounts",
                schema: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "client_accounts",
                schema: "ClientAccounts");
        }
    }
}
