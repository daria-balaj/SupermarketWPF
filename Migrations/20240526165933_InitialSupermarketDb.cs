using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket.Migrations
{
    public partial class InitialSupermarketDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestockDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyingPrice = table.Column<float>(type: "real", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProducerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Producers_ProducerName",
                        column: x => x.ProducerName,
                        principalTable: "Producers",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    Emitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CashierID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receipts_Users_CashierID",
                        column: x => x.CashierID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateOnly>(type: "date", nullable: false),
                    End = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Discounts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptItem",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiptID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Subtotal = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductID",
                table: "Discounts",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProducerName",
                table: "Products",
                column: "ProducerName");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ProductID",
                table: "ReceiptItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ReceiptID",
                table: "ReceiptItem",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CashierID",
                table: "Receipts",
                column: "CashierID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "ReceiptItem");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
