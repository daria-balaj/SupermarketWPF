using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiptItem",
                table: "ReceiptItem");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptItem_ReceiptID",
                table: "ReceiptItem");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ReceiptItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiptItem",
                table: "ReceiptItem",
                columns: new[] { "ReceiptID", "ProductID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiptItem",
                table: "ReceiptItem");

            migrationBuilder.AddColumn<string>(
                name: "ID",
                table: "ReceiptItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiptItem",
                table: "ReceiptItem",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ReceiptID",
                table: "ReceiptItem",
                column: "ReceiptID");
        }
    }
}
