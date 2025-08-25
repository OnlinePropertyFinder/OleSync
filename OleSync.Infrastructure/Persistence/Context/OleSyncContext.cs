using Microsoft.EntityFrameworkCore;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Shared.Enums;
using OleSync.Domain.People.Core.Entities;

namespace OleSync.Infrastructure.Persistence.Context
{
    public partial class OleSyncContext : DbContext
    {
        public OleSyncContext() { }

        public OleSyncContext(DbContextOptions<OleSyncContext> options)
            : base(options) { }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<BoardMember> BoardMembers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Committee> Committees { get; set; }

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

                entity.HasMany(e => e.Members)
                      .WithOne(m => m.Board)
                      .HasForeignKey(m => m.BoardId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Many-to-many: Board <-> Committee via join table BoardCommittees
                entity.HasMany(e => e.Committees)
                      .WithMany(c => c.Boards)
                      .UsingEntity<Dictionary<string, object>>(
                          "BoardCommittees",
                          j => j
                              .HasOne<Committee>()
                              .WithMany()
                              .HasForeignKey("CommitteeId")
                              .OnDelete(DeleteBehavior.Cascade),
                          j => j
                              .HasOne<Board>()
                              .WithMany()
                              .HasForeignKey("BoardId")
                              .OnDelete(DeleteBehavior.Cascade))
                      .ToTable("BoardCommittees");
            });

            modelBuilder.Entity<BoardMember>(entity =>
            {
                entity.ToTable("BoardMembers");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MemberType).IsRequired();

                entity.HasIndex(e => new { e.BoardId, e.EmployeeId, e.GuestId });

                entity.HasQueryFilter(m => !m.Audit.IsDeleted);

                entity.OwnsOne(e => e.Audit, audit =>
                {
                    audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                    audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
                    audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
                    audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
                    audit.Property(a => a.IsDeleted).HasDefaultValue(false);
                    audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                    audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
                });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.MemberType).IsRequired();

                entity.OwnsOne(e => e.Audit, audit =>
                {
                    audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                    audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
                    audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
                    audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
                    audit.Property(a => a.IsDeleted).HasDefaultValue(false);
                    audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                    audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
                });
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.ToTable("Guests");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Position).HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.MemberType).IsRequired();

                entity.OwnsOne(e => e.Audit, audit =>
                {
                    audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                    audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
                    audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
                    audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
                    audit.Property(a => a.IsDeleted).HasDefaultValue(false);
                    audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                    audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
                });
            });

            modelBuilder.Entity<Committee>(entity =>
            {
                entity.ToTable("Committees");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Purpose).HasMaxLength(500);

                entity.OwnsOne(e => e.Audit, audit =>
                {
                    audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                    audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
                    audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
                    audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
                    audit.Property(a => a.IsDeleted).HasDefaultValue(false);
                    audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                    audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
                });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}