using Microsoft.EntityFrameworkCore;
using mega_webAPI.models;
using System.Linq;
namespace mega_webAPI.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Tvshow> Tvshows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TvShowCategory> TvShowCategories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movies");
                entity.HasKey(e => e.MovieId);
                entity.Property(e => e.MovieId).HasColumnName("movie_id");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("image_url");
                entity.Property(e => e.VideoUrl).HasMaxLength(255).HasColumnName("video_url");
                entity.Property(e => e.AddedDate).HasDefaultValueSql("GETDATE()").HasColumnName("added_date");

                //relation with the categories table
                entity.HasMany(e => e.MovieCategories)
               .WithOne(mc => mc.Movie)
               .HasForeignKey(mc => mc.MovieId);

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
                entity.HasKey(e => new { e.MovieId, e.CategoryId });
                entity.Property(e => e.MovieId).HasColumnName("movie_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                //relation with moviescategories table
                entity.HasOne(mc => mc.Movie)
               .WithMany(m => m.MovieCategories)
               .HasForeignKey(mc => mc.MovieId);

                //relation with categories table
                entity.HasOne(mc => mc.Category)
               .WithMany(c => c.MovieCategories)
               .HasForeignKey(mc => mc.CategoryId);
            });

            //nos for tvshows table
            modelBuilder.Entity<Tvshow>(entity =>
            {
                entity.ToTable("tvshows");
                entity.HasKey(e => e.TvShowId);
                entity.Property(e => e.TvShowId).HasColumnName("tvshow_id");
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
                entity.Property(e => e.ImageUrl).HasMaxLength(255).HasColumnName("image_url");
                entity.Property(e => e.VideoUrl).HasMaxLength(255).HasColumnName("video_url");
                entity.Property(e => e.AddedDate).HasDefaultValueSql("GETDATE()").HasColumnName("added_date");

                //relation with the categories table
                entity.HasMany(e => e.TvShowCategories)
               .WithOne(tc => tc.Tvshow)
               .HasForeignKey(tc => tc.TvShowId);

            });


            modelBuilder.Entity<TvShowCategory>(entity =>
            {
                entity.ToTable("tvshowscategories");
                entity.HasKey(tc => new { tc.TvShowId, tc.CategoryId });

                //relation with tvshowscategories table
                entity.HasOne(tc => tc.Tvshow)
               .WithMany(t => t.TvShowCategories)
               .HasForeignKey(tc => tc.TvShowId);

                //relation with categories table
                entity.HasOne(tc => tc.Category)
               .WithMany(c => c.TvShowCategories)
               .HasForeignKey(tc => tc.CategoryId);
            });

        }
    }
}
