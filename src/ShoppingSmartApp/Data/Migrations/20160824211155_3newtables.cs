using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingSmartApp.Data.Migrations
{
    public partial class _3newtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "SuperMarkets");

            migrationBuilder.CreateTable(
                name: "ProductCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    SuperMarketId = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCatalogs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatalogs_SuperMarkets_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "SuperMarkets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateList = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Priority = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    ShoppingListId = table.Column<int>(nullable: false),
                    SuperMarketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingProducts_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingProducts_SuperMarkets_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "SuperMarkets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogs_ProductId",
                table: "ProductCatalogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogs_SuperMarketId",
                table: "ProductCatalogs",
                column: "SuperMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ProductId",
                table: "ShoppingProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ShoppingListId",
                table: "ShoppingProducts",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_SuperMarketId",
                table: "ShoppingProducts",
                column: "SuperMarketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCatalogs");

            migrationBuilder.DropTable(
                name: "ShoppingProducts");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "SuperMarkets",
                nullable: true);
        }
    }
}
