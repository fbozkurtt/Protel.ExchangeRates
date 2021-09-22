using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Protel.ExchangeRates.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrossOrder = table.Column<int>(type: "int", nullable: true),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<short>(type: "smallint", nullable: false),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForexBuying = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ForexSelling = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BanknoteBuying = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BanknoteSelling = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CrossRateUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CrossRateOther = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchangeRateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
