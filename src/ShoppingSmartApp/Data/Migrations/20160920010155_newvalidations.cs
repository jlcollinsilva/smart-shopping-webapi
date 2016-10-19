using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingSmartApp.Data.Migrations
{
    public partial class newvalidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "SuperMarkets",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "ShoppingLists",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductCatalogs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "SuperMarkets",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "ShoppingLists",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductCatalogs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                nullable: false);
        }
    }
}
