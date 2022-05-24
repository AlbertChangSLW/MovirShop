using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext 
    {
        public MovieShopDbContext( DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Trailer> Trailer { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Crew> Crew { get; set; }
        public DbSet<MovieCrew> MovieCrew { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }
        public DbSet<User> User { get; set; }   
        public DbSet<Review> Review { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Purchase> Purchase { get; set; }  
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureGenre(EntityTypeBuilder<Genre > builder)
        {
            builder.ToTable("Genre");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TrailerUrl).HasMaxLength(2084);
            builder.Property(x => x.Name).HasMaxLength(2084);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.CrewId, x.Department, x.Job });
            builder.Property(x => x.Department).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Job).HasMaxLength(128).IsRequired();
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.CastId, x.Character });
            builder.Property(x => x.Character).HasMaxLength(450).IsRequired();
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(128);
            builder.Property(x => x.LastName).HasMaxLength(128);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.HashedPassword).HasMaxLength(1024);
            builder.Property(x => x.Salt).HasMaxLength(1024);
            builder.Property(x => x.PhoneNumber).HasMaxLength(16);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.UserId});
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(x => x.Id);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(20);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
