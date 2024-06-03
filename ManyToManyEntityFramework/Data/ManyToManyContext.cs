using ManyToManyApp.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ManyToManyApp.Data
{
    public class ManyToManyContext : DbContext
    {
        public ManyToManyContext(DbContextOptions<ManyToManyContext> options) : base(options)
        {

        }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Boek> Boeken { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BoekGenre> BoekGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // configure boek 
            modelBuilder.Entity<Boek>()
                .HasKey(b => b.BoekId);
            modelBuilder.Entity<Boek>()
                .HasOne(b => b.Auteur)
                .WithMany(bg => bg.Boeken)
                .HasForeignKey(b => b.AuteurId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Boek>()
                .HasMany(b => b.BoekGenres)
                .WithOne(bg => bg.Boek)
                .HasForeignKey(b => b.BoekId)
                .OnDelete(DeleteBehavior.NoAction);

            //Configure Auteur

            modelBuilder.Entity<Auteur>()
                .HasKey(a => a.AuteurId);
            modelBuilder.Entity<Auteur>()
                .Property(a => a.Naam)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Genre
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.GenreId);

            modelBuilder.Entity<Genre>()
                .Property(g => g.Naam)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.BoekGenres)
                .WithOne(bg => bg.Genre)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure BoekGenre
            modelBuilder.Entity<BoekGenre>()
                .HasKey(bg => new { bg.BoekId, bg.GenreId });

            modelBuilder.Entity<BoekGenre>()
                .HasOne(bg => bg.Boek)
                .WithMany(b => b.BoekGenres)
                .HasForeignKey(bg => bg.BoekId);

            modelBuilder.Entity<BoekGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BoekGenres)
                .HasForeignKey(bg => bg.GenreId);

            SeedData.AddRecords(modelBuilder);

        }

    }
}