using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace PopcornBackend.Models
{
    public class MajorProjectDbContext : DbContext
    {
        private IConfiguration config;
        public MajorProjectDbContext(IConfiguration config)
        {
            this.config = config;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<TvShow> TvShows { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Singer> Singers { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<UserMovie> UserMovies { get; set; }
        public virtual DbSet<UserSong> UserSongs { get; set; }
        public virtual DbSet<UserTvShow> UserTvShows { get; set; }
        public virtual DbSet<ClientMediaType> ClientMediaTypes { get; set; }
        public virtual DbSet<MediaCategory> MediaCategories { get; set; }
        public virtual DbSet<SongSinger> SongSingers { get; set; }



        //getting connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer(config.GetConnectionString("connection"));


        //mapping of tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many mapping

            //user table
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.ToTable("Users");
                

                entity.HasIndex(entity=> entity.Email);

                entity.Property(u => u.Id)
                .HasColumnName("Id");

                entity.Property(u => u.Name)
                .HasColumnName("Name")
                .HasMaxLength(100);

                entity.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(100);

                entity.Property(u => u.Password)
                .HasColumnName("Password");

                entity.Property(u => u.MobileNo)
                .HasColumnName("MobileNo")
                .HasMaxLength(20);

                entity.Property(u => u.AlternateMobileNo)
                .HasColumnName("AlternateMobileNo")
                .HasMaxLength(20);

                entity.Property(u => u.Role)
                .HasColumnName("Role")
                .HasDefaultValue("User");

                entity.Property(u => u.ProfilePicture)
                .HasColumnName("ProfilePicture")
                .HasMaxLength(300);

                entity.Property(u => u.IsApproved)
                .HasColumnName("IsApproved")
                .HasDefaultValue(0);

                entity.HasOne(u => u.SubscriptionRef)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SubscriptionId);

                entity.Property(u => u.SubscriptionStart)
                .HasColumnName("SubscriptionStart");

                entity.Property(u => u.SubscriptionEnd)
                .HasColumnName("SubscriptionEnd");

                entity.Property(u => u.Timestamp)
                .HasColumnName("Timestamp");
            });
            
            //subscription table
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.SubscriptionId);
                entity.ToTable("Subscriptions");

                entity.Property(s => s.SubscriptionId).HasColumnName("SubscriptionId");

                entity.Property(e => e.PlanName).HasColumnName("PlanName")
                .HasMaxLength(20);

                entity.Property(e => e.Duration).HasColumnName("Duration");

                entity.Property(e => e.Price).HasColumnName("Price");

            });

            //movie table
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.MovieId);
                entity.ToTable("Movies");

                entity.HasIndex(m => m.MovieName);

                entity.Property(m => m.MovieId)
                .HasColumnName("MovieId");

                entity.Property(m => m.MovieName).HasColumnName("MovieName")
                .HasMaxLength(200);

                entity.Property(m => m.MoviePath)
                .HasMaxLength(1000)
                .HasColumnName("MoviePath");

                entity.Property(m => m.MoviePoster)
                .HasMaxLength(1000)
                .HasColumnName("MoviePoster");

                entity.Property(m => m.MovieDescription)
                .HasMaxLength(1000)
                .HasColumnName("MovieDescription");

                entity.HasOne(m => m.User)
                .WithMany(c => c.Movie)
                .HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

                //categroy fk
                entity.HasOne(m => m.CategoryRef)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryId);

                entity.Property(m => m.Likes)
                .HasColumnType("int")
                .HasColumnName("Likes");
            });

            //mediaCategories
            modelBuilder.Entity<MediaCategory>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.ToTable("MediaCategories");

                entity.Property(m => m.CategoryId)
                .HasColumnName("CategoryId");

                entity.Property(c => c.CategoryName)
                .HasColumnName("CategoryName")
                .HasMaxLength(200);
            });

            //many to many favMovies
            modelBuilder.Entity<UserMovie>(entity =>
            {
                entity.HasKey(um => new { um.UserId, um.MovieId });

                entity.ToTable("FavMovies");

                entity.HasOne(um => um.UserRef)
                .WithMany(u => u.UserMovie)
                .HasForeignKey(um => um.UserId);

                entity.HasOne(um => um.MovieRef)
                .WithMany(u => u.UserMovie)
                .HasForeignKey(um => um.MovieId).OnDelete(DeleteBehavior.NoAction);
            });

            // Tv Show
            modelBuilder.Entity<TvShow>(entity =>
            {
                entity.HasKey(s => s.ShowId);
                entity.ToTable("TvShows");

                entity.HasIndex(entity => entity.ShowName);

                entity.Property(s => s.ShowId)
                .HasColumnName("TvShowId");

                entity.Property(s => s.ShowName).HasColumnName("TvShowName")
                .HasMaxLength(200);

                entity.Property(s => s.ShowPath)
                .HasMaxLength(1000)
                .HasColumnName("TvShowPath");

                entity.Property(s => s.ShowPoster)
                .HasMaxLength(1000)
                .HasColumnName("TvShowPoster");

                entity.Property(s => s.ShowDescription)
                .HasMaxLength(1000)
                .HasColumnName("TvShowDescription");

                entity.HasOne(m => m.User)
                .WithMany(c => c.TvShow)
                .HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

                //categroy fk
                entity.HasOne(s => s.CategoryRef)
                .WithMany(c => c.TvShows)
                .HasForeignKey(m => m.CategoryId);

                entity.Property(s => s.Likes)
                .HasColumnType("int")
                .HasColumnName("Likes");
            });

            //many to many FavTvShow
            modelBuilder.Entity<UserTvShow>(entity =>
            {
                entity.HasKey(um => new { um.UserId, um.TvShowId });

                entity.ToTable("FavTvShow");

                entity.HasOne(um => um.UserRef)
                .WithMany(u => u.UserTvShow)
                .HasForeignKey(um => um.UserId);

                entity.HasOne(um => um.TvShowRef)
                .WithMany(u => u.UserTvShow)
                .HasForeignKey(um => um.TvShowId).OnDelete(DeleteBehavior.NoAction);
            });

            // Song
            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasKey(m => m.SongId);
                entity.ToTable("Songs");

                entity.HasIndex(entity => entity.SongName);
                entity.HasIndex(entity => entity.SongLyrics);

                entity.Property(m => m.SongId)
                .HasColumnName("SongId");

                entity.Property(m => m.SongName).HasColumnName("SongName")
                .HasMaxLength(200);

                entity.Property(m => m.SongPath)
                .HasMaxLength(1000)
                .HasColumnName("SongPath");

                entity.Property(m => m.SongPoster)
                .HasMaxLength(1000)
                .HasColumnName("SongPoster");

                entity.Property(m => m.SongDescription)
                .HasMaxLength(1000)
                .HasColumnName("SongDescription");

                entity.HasOne(m => m.User)
                .WithMany(c => c.Song)
                .HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

                //categroy fk
                entity.HasOne(m => m.CategoryRef)
                .WithMany(c => c.Songs)
                .HasForeignKey(m => m.CategoryId);

                entity.Property(m => m.SongGeneration)
                .HasMaxLength(100)
                .HasColumnName("SongGeneration");

                entity.Property(m => m.SongLyrics)
                .HasMaxLength(2000)
                .HasColumnName("SongLyrics");

                
                entity.Property(m => m.SongType)
                .HasMaxLength(2000)
                .HasColumnName("SongType");

                entity.Property(m => m.Likes)
                .HasColumnType("int")
                .HasColumnName("Likes");
            });

            //Singer
            modelBuilder.Entity<Singer>(entity =>
            {
                entity.HasKey(c => c.SingerId);
                entity.ToTable("Singers");

                entity.Property(m => m.SingerId)
                .HasColumnName("SingerId");

                entity.Property(c => c.SingerName)
                .HasColumnName("SingerName")
                .HasMaxLength(200);
            });

            //many to many SongSinger
            modelBuilder.Entity<SongSinger>(entity =>
            {
                entity.HasKey(um => new { um.SongId, um.SingerId });

                entity.ToTable("SongSingers");

                entity.HasOne(um => um.SongRef)
                .WithMany(u => u.SongSinger)
                .HasForeignKey(um => um.SongId);

                entity.HasOne(um => um.SingerRef)
                .WithMany(u => u.SongSinger)
                .HasForeignKey(um => um.SingerId);
            });

            //many to many FavSongs
            modelBuilder.Entity<UserSong>(entity =>
            {
                entity.HasKey(um => new { um.UserId, um.SongId });

                entity.ToTable("FavSongs");

                entity.HasOne(um => um.UserRef)
                .WithMany(u => u.UserSong)
                .HasForeignKey(um => um.UserId);

                entity.HasOne(um => um.SongRef)
                .WithMany(u => u.UserSong)
                .HasForeignKey(um => um.SongId).OnDelete(DeleteBehavior.NoAction);
            });

            //MediaType
            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.HasKey(c => c.MediaId);
                entity.ToTable("MediaTypes");


                entity.Property(e => e.MediaId)
                .HasColumnName("MediaId");

                entity.Property(c => c.MediaName)
                .HasColumnName("MediaName")
                .HasMaxLength(200);
            });

            // many to many client with media type
            modelBuilder.Entity<ClientMediaType>(entity =>
            {
                entity.HasKey(um => new { um.ClientId, um.MediaId });

                entity.ToTable("ClientsMedia");

                entity.HasOne(um => um.UserRef)
                .WithMany(u => u.ClientMediaType)
                .HasForeignKey(um => um.ClientId);

                entity.HasOne(um => um.MediaTypeRef)
                .WithMany(u => u.ClientMediaType)
                .HasForeignKey(um => um.MediaId);
            });

        }

    }
}
