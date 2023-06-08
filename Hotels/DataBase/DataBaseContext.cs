using Hotels.Models;
using Hotels.Models.Categorie;
using Hotels.Models.Hotel;
using Hotels.Models.Hotel_Categorie;
using Hotels.Models.Review;
using Microsoft.EntityFrameworkCore;

namespace Hotels.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
         public DbSet <Hotel_Categorie> Hotel_Categorie { get; set; }
        public DbSet <Review> Review { get; set; }
        public DbSet <Hotel> Hotel { get; set; }




        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel_Categorie>()
                .HasKey(h_c => new { h_c.CategorieId, h_c.HotelId });

            modelBuilder.Entity<Hotel_Categorie>()
                .HasOne(h_c => h_c.Hotel)
                .WithMany(c =>c.hotel_categories)
                .HasForeignKey(h => h.HotelId);

            modelBuilder.Entity<Hotel_Categorie>()
                .HasOne(aux =>aux.Categorie)
                .WithMany(aux => aux.hotel_categorie)
                .HasForeignKey(w => w.CategorieId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.reviews)
                .WithOne(r => r.hotel);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.hotel)
                .WithMany(h => h.reviews);


           


              
        }
    }
}
