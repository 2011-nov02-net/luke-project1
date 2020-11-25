using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreApplication.WebApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "StoreApp");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "StoreApp",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "StoreApp",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "StoreApp",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    OrderLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "StoreApp",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__Orders__Customer__6D9742D9",
                        column: x => x.CustomerId,
                        principalSchema: "StoreApp",
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Orders__Location__6CA31EA0",
                        column: x => x.LocationId,
                        principalSchema: "StoreApp",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreInventory",
                schema: "StoreApp",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInventory", x => new { x.LocationId, x.ProductId });
                    table.ForeignKey(
                        name: "FK__StoreInve__Locat__7167D3BD",
                        column: x => x.LocationId,
                        principalSchema: "StoreApp",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__StoreInve__Produ__725BF7F6",
                        column: x => x.ProductId,
                        principalSchema: "StoreApp",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSale",
                schema: "StoreApp",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SalePrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleId", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK__OrderSale__Order__762C88DA",
                        column: x => x.OrderId,
                        principalSchema: "StoreApp",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderSale__Produ__7720AD13",
                        column: x => x.ProductId,
                        principalSchema: "StoreApp",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__A9D1053461B38AC1",
                schema: "StoreApp",
                table: "Customer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Location__737584F6D233EE55",
                schema: "StoreApp",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "StoreApp",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                schema: "StoreApp",
                table: "Orders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSale_ProductId",
                schema: "StoreApp",
                table: "OrderSale",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UQ__Product__737584F621731BB2",
                schema: "StoreApp",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventory_ProductId",
                schema: "StoreApp",
                table: "StoreInventory",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSale",
                schema: "StoreApp");

            migrationBuilder.DropTable(
                name: "StoreInventory",
                schema: "StoreApp");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "StoreApp");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "StoreApp");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "StoreApp");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "StoreApp");
        }
    }
}
