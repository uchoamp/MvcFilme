using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFilme.Migrations
{
    public partial class enum_classificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Lancamento",
                table: "Filme",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<byte>(
                name: "Classificacao",
                table: "Filme",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<byte>(
                name: "UnidadeFederativa",
                table: "Cinema",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FimExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Lancamento",
                table: "Filme",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "Classificacao",
                table: "Filme",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "UnidadeFederativa",
                table: "Cinema",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExibicao",
                table: "Cartaz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FimExibicao",
                table: "Cartaz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }
    }
}
