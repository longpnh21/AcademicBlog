using Core.Common;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infrastructure.Data
{
    public partial class AcademicBlogContext : IdentityDbContext<User>
    {
        public AcademicBlogContext()
        {
        }

        public AcademicBlogContext(DbContextOptions<AcademicBlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Blogs { get; set; }
        public virtual DbSet<BlogCategory> BlogCategories { get; set; }
        public virtual DbSet<BlogTag> BlogTags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.ApproverId).HasMaxLength(300);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.CreatorId)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.ModifiedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.BlogApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Blog_Users1");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.BlogCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_Users");
            });

            modelBuilder.Entity<BlogCategory>((System.Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BlogCategory>>)(entity =>
            {
                entity.HasKey(e => (new { e.BlogId, e.CategoryId }));

                entity.ToTable("BlogCategory");

                entity.HasOne(d => d.Blog)
                    .WithMany((System.Linq.Expressions.Expression<System.Func<Category, System.Collections.Generic.IEnumerable<BlogCategory>>>)(p => (System.Collections.Generic.IEnumerable<BlogCategory>)p.BlogCategories))
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_BlogCategory_Blog");

                entity.HasOne(d => d.Category)
                    .WithMany((System.Linq.Expressions.Expression<System.Func<Category, System.Collections.Generic.IEnumerable<BlogCategory>>>)(p => (System.Collections.Generic.IEnumerable<BlogCategory>)p.BlogCategories))
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BlogCategory_Category");
            }));

            modelBuilder.Entity<BlogTag>(entity =>
            {
                entity.HasKey(e => new { e.BlogId, e.TagId });

                entity.ToTable("BlogTag");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogTags)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_BlogTag_Blog");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.BlogTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_BlogTag_Tag");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Comment_Blog");

                entity.HasOne(d => d.Reference)
                    .WithMany(p => p.InverseReference)
                    .HasForeignKey(d => d.ReferenceId)
                    .HasConstraintName("FK_Comment_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comment_Users");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Media_Blog");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(500);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BlogId });

                entity.ToTable("Vote");

                entity.Property(e => e.UserId).HasMaxLength(300);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Vote_Blog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Vote_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
