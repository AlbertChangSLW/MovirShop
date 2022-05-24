using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updatePurchaseTableColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDataTime",
                table: "Purchase",
                newName: "PurchaseDateTime");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseNumber",
                table: "Purchase",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDateTime",
                table: "Purchase",
                newName: "PurchaseDataTime");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseNumber",
                table: "Purchase",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
