using Microsoft.EntityFrameworkCore;
using mitfahr.Models;

namespace mitfahr.Data
{
    public partial class mitfahrgelegenheitContext : DbContext
    {
        public mitfahrgelegenheitContext(DbContextOptions<mitfahrgelegenheitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeparturePoint> DeparturePoint { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<JourneyHasUser> JourneyHasUser { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=92.51.164.28;port=3306;database=mitfahrgelegenheit;uid=mitfahradmin;password=zbTbenfqxpkP6ad", x => x.ServerVersion("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeparturePoint>(entity =>
            {
                entity.HasKey(e => e.Postcode)
                    .HasName("PRIMARY");

                entity.Property(e => e.Postcode).HasColumnName("postcode");

                entity.Property(e => e.City)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.HasKey(e => new { e.Idjourney, e.DeparturePointPostcode, e.UserIdUser })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.HasIndex(e => e.DeparturePointPostcode)
                    .HasName("fk_Journey_DeparturePoint1_idx");

                entity.HasIndex(e => e.UserIdUser)
                    .HasName("fk_Journey_User1_idx");

                entity.Property(e => e.Idjourney).HasColumnName("idjourney");

                entity.Property(e => e.DeparturePointPostcode).HasColumnName("DeparturePoint_postcode");

                entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

                entity.Property(e => e.DepartureTime)
                    .IsRequired()
                    .HasColumnName("departure_time")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Regularly)
                    .IsRequired()
                    .HasColumnName("regularly")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Smoker).HasColumnName("smoker");

                entity.HasOne(d => d.DeparturePointPostcodeNavigation)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.DeparturePointPostcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journey_DeparturePoint1");

                entity.HasOne(d => d.UserIdUserNavigation)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.UserIdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journey_User1");
            });

            modelBuilder.Entity<JourneyHasUser>(entity =>
            {
                entity.HasKey(e => new { e.JourneyIdjourney, e.JourneyDeparturePointPostcode, e.JourneyUserIdUser, e.UserIdUser })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("Journey_has_User");

                entity.HasIndex(e => e.UserIdUser)
                    .HasName("fk_Journey_has_User_User1_idx");

                entity.HasIndex(e => new { e.JourneyIdjourney, e.JourneyDeparturePointPostcode, e.JourneyUserIdUser })
                    .HasName("fk_Journey_has_User_Journey1_idx")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.Property(e => e.JourneyIdjourney).HasColumnName("Journey_idjourney");

                entity.Property(e => e.JourneyDeparturePointPostcode).HasColumnName("Journey_DeparturePoint_postcode");

                entity.Property(e => e.JourneyUserIdUser).HasColumnName("Journey_User_idUser");

                entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

                entity.HasOne(d => d.UserIdUserNavigation)
                    .WithMany(p => p.JourneyHasUser)
                    .HasForeignKey(d => d.UserIdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journey_has_User_User1");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.JourneyHasUser)
                    .HasForeignKey(d => new { d.JourneyIdjourney, d.JourneyDeparturePointPostcode, d.JourneyUserIdUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Journey_has_User_Journey1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.EMail)
                    .HasName("eMail_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("eMail")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
