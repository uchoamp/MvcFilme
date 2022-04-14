using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFilme.Migrations
{
    public partial class time_register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Filme",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Filme",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Cinema",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Cinema",
                nullable: false,
                defaultValueSql: "GETDATE()");


            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Cartaz",
                type: "Decimal(4,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Decimal(4,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FimExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Cartaz",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Cartaz",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.Sql(@"
                            CREATE TRIGGER Filme_LASTUPDATED 
                            ON [Dbo].[Filme] AFTER UPDATE
                            AS
                            BEGIN
                                SET NOCOUNT ON;
                                UPDATE [Dbo].[Filme] SET LastUpdated = GETDATE() WHERE Id = (SELECT Id from INSERTED);
                            END
                            GO

                            CREATE TRIGGER Cinema_LASTUPDATED 
                            ON [Dbo].[Cinema] AFTER UPDATE
                            AS
                            BEGIN
                                SET NOCOUNT ON;
                                UPDATE [Dbo].[Cinema] SET LastUpdated = GETDATE() WHERE Id = (SELECT Id from INSERTED);
                            END
                            GO

                            CREATE TRIGGER Cartaz_LASTUPDATED 
                            ON [Dbo].[Cartaz] AFTER UPDATE
                            AS
                            BEGIN
                                SET NOCOUNT ON;
                                UPDATE [Dbo].[Cartaz] SET LastUpdated = GETDATE() WHERE Id = (SELECT Id from INSERTED);
                            END
                            GO
                            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Filme");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Filme");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Cinema");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Cinema");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Cartaz");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Cartaz");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Cartaz",
                type: "Decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(4,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FimExibicao",
                table: "Cartaz",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
            migrationBuilder.Sql(@"
                        DROP TRIGGER IF EXISTS Filme_LASTUPDATED;
                        DROP TRIGGER IF EXISTS Cinema_LASTUPDATED;
                        DROP TRIGGER IF EXISTS Cartaz_LASTUPDATED;
            ");
        }
    }
}
