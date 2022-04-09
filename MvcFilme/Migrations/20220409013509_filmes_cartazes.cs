using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFilme.Migrations
{
    public partial class filmes_cartazes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(maxLength: 100, nullable: false),
                    UnidadeFederativa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Titulo = table.Column<string>(maxLength: 60, nullable: false),
                    Sinopse = table.Column<string>(type: "text", nullable: true),
                    Capa = table.Column<string>(maxLength: 255, nullable: false),
                    Lancamento = table.Column<DateTime>(nullable: false),
                    Genero = table.Column<string>(maxLength: 30, nullable: false),
                    Classificacao = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartaz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Preco = table.Column<decimal>(type: "Decimal(2,2)", nullable: false),
                    InicioExibicao = table.Column<DateTime>(nullable: false),
                    FimExibicao = table.Column<DateTime>(nullable: false),
                    FilmeId = table.Column<int>(nullable: false),
                    CinemaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartaz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartaz_Cinema_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cartaz_Filme_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartaz_CinemaId",
                table: "Cartaz",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartaz_FilmeId",
                table: "Cartaz",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_PublicId",
                table: "Filme",
                column: "PublicId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartaz");

            migrationBuilder.DropTable(
                name: "Cinema");

            migrationBuilder.DropTable(
                name: "Filme");
        }
    }
}
