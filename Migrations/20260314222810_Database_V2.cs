using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vivigest_backend.Migrations
{
    /// <inheritdoc />
    public partial class Database_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargeAccounts",
                columns: table => new
                {
                    IdChargeAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    IdPaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    concept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeAccounts", x => x.IdChargeAccount);
                    table.ForeignKey(
                        name: "FK_ChargeAccounts_PaymentPeriods_IdPaymentPeriod",
                        column: x => x.IdPaymentPeriod,
                        principalTable: "PaymentPeriods",
                        principalColumn: "IdPaymentPeriod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChargeAccounts_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "IdUnit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeAccounts_IdPaymentPeriod",
                table: "ChargeAccounts",
                column: "IdPaymentPeriod");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeAccounts_IdUnit",
                table: "ChargeAccounts",
                column: "IdUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeAccounts");
        }
    }
}
