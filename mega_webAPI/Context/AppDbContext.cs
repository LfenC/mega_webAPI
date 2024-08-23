using Microsoft.EntityFrameworkCore;
using mega_webAPI.Data.models;
using System.Linq;

namespace mega_webAPI.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Tvshow> Tvshows { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TvShowCategory> TvShowCategories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<TvShowGenre> TvShowGenres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("movie_id");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("image_url");
                entity.Property(e => e.VideoUrl).HasMaxLength(255).HasColumnName("video_url");
                entity.Property(e => e.AddedDate).HasDefaultValueSql("GETDATE()").HasColumnName("added_date");

                //relation with the categories table
                entity.HasMany(e => e.MovieCategories)
               .WithOne(mc => mc.Movie)
               .HasForeignKey(mc => mc.Id);

                entity.HasMany(e => e.Genres)
                .WithOne(mg => mg.Movie)
                .HasForeignKey(mg => mg.Id);

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("name");

                //relation with moviescategories table
                entity.HasMany(e => e.MovieCategories)
               .WithOne(mc => mc.Category)
               .HasForeignKey(mc => mc.CategoryId);

                //tvshows categories
                entity.HasMany(e => e.TvShowCategories)
                .WithOne(tc => tc.Category)
                .HasForeignKey(tc => tc.CategoryId);

            });

            modelBuilder.Entity<MovieCategory>(entity =>
            {
                entity.ToTable("moviescategories");
                entity.HasKey(e => new { e.Id, e.CategoryId });
                entity.Property(e => e.Id).HasColumnName("movie_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                //relation with moviescategories table
                entity.HasOne(mc => mc.Movie)
               .WithMany(m => m.MovieCategories)
               .HasForeignKey(mc => mc.Id);

                //relation with categories table
                entity.HasOne(mc => mc.Category)
               .WithMany(c => c.MovieCategories)
               .HasForeignKey(mc => mc.CategoryId);
            });

            //nos for tvshows table
            modelBuilder.Entity<Tvshow>(entity =>
            {
                entity.ToTable("tvshows");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("tvshow_id");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("image_url");
                entity.Property(e => e.VideoUrl).HasMaxLength(255).HasColumnName("video_url");
                entity.Property(e => e.AddedDate).HasDefaultValueSql("GETDATE()").HasColumnName("added_date");

                //relation with the categories table
                entity.HasMany(e => e.TvShowCategories)
               .WithOne(tc => tc.Tvshow)
               .HasForeignKey(tc => tc.tvShowId);

            });


            modelBuilder.Entity<TvShowCategory>(entity =>
            {
                entity.ToTable("tvshowscategories");
                entity.HasKey(tc => new { tc.tvShowId, tc.CategoryId });

                //relation with tvshowscategories table
                entity.HasOne(tc => tc.Tvshow)
               .WithMany(t => t.TvShowCategories)
               .HasForeignKey(tc => tc.tvShowId);

                //relation with categories table
                entity.HasOne(tc => tc.Category)
               .WithMany(c => c.TvShowCategories)
               .HasForeignKey(tc => tc.CategoryId);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {   entity.ToTable("moviesgenres");
                entity.HasKey(mg => new { mg.Id, mg.GenreId });

                entity.HasOne(mg => mg.Movie)
                .WithMany(m => m.Genres)
                .HasForeignKey(mg  => mg.Id);

                entity.HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");
                entity.HasKey(g => g.GenreId);
                entity.Property(g => g.GenreId).HasColumnName("genre_id");
                entity.Property(g => g.Name).IsRequired().HasMaxLength(56);

                entity.HasMany(g => g.MovieGenres)
                .WithOne(mg => mg.Genre)
                .HasForeignKey(mg => mg.GenreId);
            });

            modelBuilder.Entity<TvShowGenre>(entity =>
            {
                entity.ToTable("tvshowsgenres");
                entity.HasKey(tg => new { tg.tvShowId, tg.GenreId });

                entity.HasOne(tg => tg.Tvshow)
                .WithMany(t => t.Genres)
                .HasForeignKey(tg => tg.tvShowId);

                entity.HasOne(tg => tg.Genre)
                .WithMany(g => g.TvShowGenres)
                .HasForeignKey(tg => tg.GenreId);
            });

          /*  modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(ma => new { ma.MovieId, ma.ActorId });

                entity.HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

                entity.HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.MovieId);
            });*/
        }
    }
}
