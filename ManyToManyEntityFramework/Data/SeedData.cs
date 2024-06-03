using ManyToManyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManyToManyApp.Data
{
    public static class SeedData
    {
        public static void AddRecords(ModelBuilder modelBuilder)
        {
            // Seeding data
            modelBuilder.Entity<Auteur>().HasData(
                new Auteur { AuteurId = 1, Naam = "Jan Jansen" },
                new Auteur { AuteurId = 2, Naam = "Sara Smit" },
                new Auteur { AuteurId = 3, Naam = "Kenan Kurda" }
            );

            modelBuilder.Entity<Genre>().HasData(
         new Genre { GenreId = 1, Naam = "Fictie" },
         new Genre { GenreId = 2, Naam = "Science Fiction" },
         new Genre { GenreId = 3, Naam = "Thriller" },
         new Genre { GenreId = 4, Naam = "Action" },
         new Genre { GenreId = 5, Naam = "Comedy" },
         new Genre { GenreId = 6, Naam = "Romance" }
     // Voeg meer genres toe zoals nodig
     );

            // Seeding Boeken
            modelBuilder.Entity<Boek>().HasData(
                new Boek { BoekId = 1, Titel = "De Ruimte Verkenner", AuteurId = 1, IsAvailable = true },
                new Boek { BoekId = 2, Titel = "Werelden Verbinden", AuteurId = 1, IsAvailable = true },
                new Boek { BoekId = 3, Titel = "De Laatste Dag", AuteurId = 2, IsAvailable = true },
                new Boek { BoekId = 4, Titel = "De Laatste Dag 2", AuteurId = 2, IsAvailable = true },
                new Boek { BoekId = 5, Titel = "De Laatste Dag 3", AuteurId = 2, IsAvailable = true }
            // Voeg meer boeken toe zoals nodig
            );

            // Seeding BoekGenre koppelingen
            modelBuilder.Entity<BoekGenre>().HasData(
                new BoekGenre { BoekId = 1, GenreId = 2 },
                new BoekGenre { BoekId = 2, GenreId = 2 },
                new BoekGenre { BoekId = 3, GenreId = 3 },
                new BoekGenre { BoekId = 4, GenreId = 3 },
                new BoekGenre { BoekId = 5, GenreId = 2 }
                );


        }
    }
}