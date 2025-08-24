using Microsoft.EntityFrameworkCore;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Infrastructure.Persistence.Context
{
    public partial class OleSyncContext : DbContext
    {
        public OleSyncContext() { }

        public OleSyncContext(DbContextOptions<OleSyncContext> options)
            : base(options) { }

        public virtual DbSet<Board> Boards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>(entity =>
            {
                entity.ToTable("Boards");

                // Add global query filter to ignore soft-deleted entities
                entity.HasQueryFilter(b => !b.Audit.IsDeleted);

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Purpose)
                    .HasMaxLength(500);

                entity.Property(e => e.BoardType);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasDefaultValue(Status.Draft);

                // Configure AuditInfo as a complex type (owned entity)
                entity.OwnsOne(e => e.Audit, audit =>
                {
                    audit.Property(a => a.CreatedBy)
                        .HasColumnName("CreatedBy"); // Optional: specify column name

                    audit.Property(a => a.CreatedAt)
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedAt");

                    audit.Property(a => a.ModifiedBy)
                        .HasColumnName("ModifiedBy");

                    audit.Property(a => a.ModifiedAt)
                        .HasColumnType("datetime")
                        .HasColumnName("ModifiedAt");

                    audit.Property(a => a.IsDeleted)
                        .HasDefaultValue(false);
                    
                    audit.Property(a => a.DeletedBy)
                        .HasColumnName("DeletedBy");

                    audit.Property(a => a.DeletedAt)
                        .HasColumnType("datetime")
                        .HasColumnName("DeletedAt");
                });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}