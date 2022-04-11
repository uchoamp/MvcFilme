using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFilme.Migrations
{
    public partial class cartaz_preco42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Cartaz",
                type: "Decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(2,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Cartaz",
                type: "Decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(4,2)");
        }
    }
}
