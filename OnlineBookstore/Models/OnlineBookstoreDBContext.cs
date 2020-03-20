using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineBookstore.Models
{
    public partial class OnlineBookstoreDBContext : DbContext
    {
        public OnlineBookstoreDBContext()
        {
        }

        public OnlineBookstoreDBContext(DbContextOptions<OnlineBookstoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MI\\SQLEXPRESS;Database=OnlineBookstoreDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.PublishDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BookInfo_Category");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId);

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.CateName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNo);

                entity.Property(e => e.OrderPrice).HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.DetailNo)
                    .HasName("PK_PurchaseInfo");

                entity.Property(e => e.DetailNo).HasColumnName("detailNO");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.OrderNo).HasColumnName("OrderNO");

                entity.Property(e => e.PurPrice).HasColumnType("money");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Purchase_Book");

                entity.HasOne(d => d.OrderNoNavigation)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.OrderNo)
                    .HasConstraintName("FK_Purchase_Order");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("PWD")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ShoppingCartNo).HasColumnName("ShoppingCartNO");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
