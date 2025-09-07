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
		public virtual DbSet<CommitteeMember> CommitteeMembers { get; set; }
		public virtual DbSet<CommitteeMeeting> CommitteeMeetings { get; set; }
        public virtual DbSet<BoardCommittee> BoardCommittees { get; set; }

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

				entity.Property(e => e.DocumentUrl)
					.HasMaxLength(500);

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
                        .HasColumnName("IsDeleted")
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

                entity.HasMany(e => e.BoardCommittees)
					  .WithOne(bc => bc.Board)
					  .HasForeignKey(bc => bc.BoardId)
					  .OnDelete(DeleteBehavior.Cascade);
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
					audit.Property(a => a.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
					audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
					audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
				});

				// Optional relationships to person sources
				entity
					.HasOne(m => m.Employee)
					.WithMany()
					.HasForeignKey(m => m.EmployeeId)
					.OnDelete(DeleteBehavior.Restrict);

				entity
					.HasOne(m => m.Guest)
					.WithMany()
					.HasForeignKey(m => m.GuestId)
					.OnDelete(DeleteBehavior.Restrict);
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
					audit.Property(a => a.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
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
					audit.Property(a => a.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
					audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
					audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
				});
			});

			modelBuilder.Entity<Committee>(entity =>
			{
				entity.ToTable("Committees");

                entity.HasQueryFilter(b => !b.Audit.IsDeleted);

                entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();
				entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
				entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CommitteeType);
                entity.Property(e => e.IsLinkedToBoard).HasDefaultValue(false);
                entity.Property(e => e.StartDate).HasColumnType("date");
                entity.Property(e => e.EndDate).HasColumnType("date");
                entity.Property(e => e.Status).HasDefaultValue(Status.Draft);
                entity.Property(e => e.DocumentUrl).HasMaxLength(500);

                entity.OwnsOne(e => e.Audit, audit =>
				{
					audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
					audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
					audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
					audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
					audit.Property(a => a.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
					audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
					audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
				});

				// Relationships
				entity
					.HasMany(e => e.Members)
					.WithOne(m => m.Committee)
					.HasForeignKey(m => m.CommitteeId)
					.OnDelete(DeleteBehavior.Cascade);

				entity
					.HasMany(e => e.Meetings)
					.WithOne(m => m.Committee)
					.HasForeignKey(m => m.CommitteeId)
					.OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.BoardCommittees)
					.WithOne(bc => bc.Committee)
					.HasForeignKey(bc => bc.CommitteeId)
					.OnDelete(DeleteBehavior.Cascade);
            });

			modelBuilder.Entity<CommitteeMember>(entity =>
			{
				entity.ToTable("CommitteeMembers");

				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.MemberType).IsRequired();
				entity.Property(e => e.Role).IsRequired();

				entity.HasIndex(e => new { e.CommitteeId, e.EmployeeId, e.GuestId });

				entity.OwnsOne(e => e.Audit, audit =>
				{
					audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
					audit.Property(a => a.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
					audit.Property(a => a.ModifiedBy).HasColumnName("ModifiedBy");
					audit.Property(a => a.ModifiedAt).HasColumnType("datetime").HasColumnName("ModifiedAt");
					audit.Property(a => a.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
					audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
					audit.Property(a => a.DeletedAt).HasColumnType("datetime").HasColumnName("DeletedAt");
				});

				entity
					.HasOne(m => m.Employee)
					.WithMany()
					.HasForeignKey(m => m.EmployeeId)
					.OnDelete(DeleteBehavior.Restrict);

				entity
					.HasOne(m => m.Guest)
					.WithMany()
					.HasForeignKey(m => m.GuestId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<CommitteeMeeting>(entity =>
			{
				entity.ToTable("CommitteeMeetings");

				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
				entity.Property(e => e.MeetingType).IsRequired();
				entity.Property(e => e.Date).HasColumnType("date");
				entity.Property(e => e.Time).HasColumnType("time");
				entity.Property(e => e.Address).HasMaxLength(500);
			});

            modelBuilder.Entity<BoardCommittee>(entity =>
            {
                entity.ToTable("BoardCommittees");

                entity.HasKey(bc => new { bc.BoardId, bc.CommitteeId });

                // Configure relationships
                entity.HasOne(bc => bc.Board)
                      .WithMany(b => b.BoardCommittees)
                      .HasForeignKey(bc => bc.BoardId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bc => bc.Committee)
                      .WithMany(c => c.BoardCommittees)
                      .HasForeignKey(bc => bc.CommitteeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}