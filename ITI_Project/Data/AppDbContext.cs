using ITI_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Programming" },
                new Category { Id = 2, Name = "Science" },
                new Category { Id = 3, Name = "History" },
                new Category { Id = 4, Name = "Literature" },
                new Category { Id = 5, Name = "Self Development" },
                new Category { Id = 6, Name = "Business" },
                new Category { Id = 7, Name = "Fantasy" },
                new Category { Id = 8, Name = "Mystery" }
            );
            // Author

            modelBuilder.Entity<Author>()
                .HasKey(a => a.Id);


            // Category

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            // Book

            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            // Author 1 ---- * Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category 1 ---- * Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // BookCopy

            modelBuilder.Entity<BookCopy>()
                .HasKey(bc => bc.Id);

            // Book 1 ---- * BookCopy
            modelBuilder.Entity<BookCopy>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Restrict);


            // Member

            modelBuilder.Entity<Member>()
                .HasKey(m => m.Id);


            // Loan

            modelBuilder.Entity<Loan>()
                .HasKey(l => l.Id);

            // Member 1 ---- * Loan
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookCopy 1 ---- * Loan
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.BookCopy)
                .WithMany(bc => bc.Loans)
                .HasForeignKey(l => l.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict);


            // Fine

            modelBuilder.Entity<Fine>()
                .HasKey(f => f.Id);

            // Loan 1 ---- 0..1 Fine
            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Loan)
                .WithOne(l => l.Fine)
                .HasForeignKey<Fine>(f => f.LoanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review

            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);

            // Book 1 ---- * Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Member 1 ---- * Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // One Member can review a Book only once
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.BookId, r.MemberId })
                .IsUnique();


            // Reservation

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);

            // Book 1 ---- * Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member 1 ---- * Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reservations)
                .HasForeignKey(r => r.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }




    }
}
