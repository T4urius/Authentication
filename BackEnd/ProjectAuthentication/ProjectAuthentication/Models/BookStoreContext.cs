using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAuthentication.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {

        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }

        public virtual DbSet<TblBook> TblBook { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=BookStore; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBook>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.BookId)
                .HasColumnName("BookID");

                entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);

                entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode()
                .HasColumnName("ISBN");

                entity.Property(e => e.Price)
                .HasColumnName("decimal(18,0)");

                entity.Property(e => e.Publisher)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode();
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                .HasColumnName("UserID");

                entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode();

                entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode();

                entity.Property(e => e.Password)
                .HasMaxLength(128);

                entity.Property(e => e.Salt)
                .HasMaxLength(128);
            });
        }
    }
}
