﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcFilme.Data;

namespace MvcFilme.Migrations
{
    [DbContext(typeof(MvcFilmeContext))]
    [Migration("20220413112157_time_register")]
    partial class time_register
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MvcFilme.Models.Cartaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CinemaId");

                    b.Property<int>("FilmeId");

                    b.Property<DateTime?>("FimExibicao")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("InicioExibicao")
                        .HasColumnType("Date");

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnUpdate();

                    b.Property<decimal?>("Preco")
                        .HasColumnType("Decimal(4,2)");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("FilmeId");

                    b.ToTable("Cartaz");
                });

            modelBuilder.Entity("MvcFilme.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnUpdate();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<byte>("UnidadeFederativa");

                    b.HasKey("Id");

                    b.ToTable("Cinema");
                });

            modelBuilder.Entity("MvcFilme.Models.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Capa")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<byte>("Classificacao");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("Lancamento")
                        .HasColumnType("Date");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnUpdate();

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Sinopse")
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("PublicId")
                        .IsUnique();

                    b.ToTable("Filme");
                });

            modelBuilder.Entity("MvcFilme.Models.Cartaz", b =>
                {
                    b.HasOne("MvcFilme.Models.Cinema", "Cinema")
                        .WithMany("Cartazes")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcFilme.Models.Filme", "Filme")
                        .WithMany("Cartazes")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
