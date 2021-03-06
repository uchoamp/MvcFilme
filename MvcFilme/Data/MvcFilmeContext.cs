using Microsoft.EntityFrameworkCore;
using MvcFilme.Models;

namespace MvcFilme.Data
{
    public class MvcFilmeContext : DbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Cartaz> Cartaz { get; set; }

        public MvcFilmeContext (DbContextOptions<MvcFilmeContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Filme>()
                .Property(f => f.PublicId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Filme>()
                .Property(f => f.Inserted)
                .HasDefaultValueSql("GETDATE()"); 
            //  .ValueGeneratedOnAdd(); // Não funciona

            modelBuilder.Entity<Filme>()
                .Property(f => f.LastUpdated)
                .HasDefaultValueSql("GETDATE()");
            //  .ValueGeneratedOnUpdate();

            modelBuilder.Entity<Cinema>()
                .Property(c => c.PublicId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Cinema>()
                .Property(c => c.Inserted)
                .HasDefaultValueSql("GETDATE()");
            //  .ValueGeneratedOnAdd();

            modelBuilder.Entity<Cinema>()
                .Property(c => c.LastUpdated)
                .HasDefaultValueSql("GETDATE()");
            //  .ValueGeneratedOnUpdate();

            modelBuilder.Entity<Cartaz>()
                .Property(c => c.PublicId)
                .HasDefaultValueSql("NEWID()");
           
            modelBuilder.Entity<Cartaz>()
                .Property(c => c.Inserted)
                .HasDefaultValueSql("GETDATE()");
            //  .ValueGeneratedOnAdd();

            modelBuilder.Entity<Cartaz>()
                .Property(c => c.LastUpdated)
                .HasDefaultValueSql("GETDATE()");
            //  .ValueGeneratedOnUpdate();

            modelBuilder.Entity<Filme>()
                .HasIndex(f => f.PublicId)
                .IsUnique();

            modelBuilder.Entity<Cinema>()
                .HasIndex(c => c.PublicId)
                .IsUnique();

            modelBuilder.Entity<Filme>()
                .HasIndex(c => c.PublicId)
                .IsUnique();

            modelBuilder.Entity<Cartaz>()
                .HasOne(c => c.Filme)
                .WithMany(f => f.Cartazes)
                .HasForeignKey(c => c.FilmeId);

            modelBuilder.Entity<Cartaz>()
                .HasOne(c => c.Cinema)
                .WithMany(cine => cine.Cartazes)
                .HasForeignKey(c => c.CinemaId);
        }
    }
}
