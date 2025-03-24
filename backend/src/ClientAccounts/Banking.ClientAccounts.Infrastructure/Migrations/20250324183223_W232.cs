using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.ClientAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class W232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "validThru",
                schema: "ClientAccounts",
                table: "cards",
                newName: "valid_thru");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "valid_thru",
                schema: "ClientAccounts",
                table: "cards",
                newName: "validThru");
        }
    }
}
