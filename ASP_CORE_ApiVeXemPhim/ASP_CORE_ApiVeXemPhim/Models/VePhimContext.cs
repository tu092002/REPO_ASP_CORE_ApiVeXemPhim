using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ASP_CORE_ApiVeXemPhim.Models
{
    public partial class VePhimContext : DbContext
    {
        public VePhimContext()
        {
        }

        public VePhimContext(DbContextOptions<VePhimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<Rap> Raps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOPACER\\SQLEXPRESS;Initial Catalog=VePhim;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ghe>(entity =>
            {
                entity.HasKey(e => e.MaGhe);

                entity.ToTable("Ghe");

                entity.Property(e => e.TenGhe).HasMaxLength(50);
            });

            modelBuilder.Entity<Phim>(entity =>
            {
                entity.HasKey(e => e.MaPhim);

                entity.ToTable("Phim");

                entity.Property(e => e.DaoDien).HasMaxLength(50);

                entity.Property(e => e.ImgPhim).HasMaxLength(50);

                entity.Property(e => e.Mota).HasMaxLength(50);

                entity.Property(e => e.TenPhim)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rap>(entity =>
            {
                entity.HasKey(e => e.MaRap);

                entity.ToTable("Rap");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.TenRap).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.MaUser);

                entity.ToTable("User");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ve>(entity =>
            {
                entity.HasKey(e => e.MaVe);

                entity.ToTable("Ve");

                entity.Property(e => e.GiaVe).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.NgayXem).HasColumnType("datetime");

                entity.HasOne(d => d.MaPhimNavigation)
                    .WithMany(p => p.Ves)
                    .HasForeignKey(d => d.MaPhim)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ve_Phim1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
