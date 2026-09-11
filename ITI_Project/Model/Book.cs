using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_Project.Model
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }="";
        [MaxLength(13)]
        public string ISBN { get; set; } = "";
        public string Summary { get; set; } = "";
        public string BookLanguage { get; set; } = "";
        public int PublishedYear { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string CoverImage { get; set; } = "";
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Author Author { get; set; } = new();
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Category Category { get; set; } = new();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Reservation>    Reservations { get; set; } = new List<Reservation>();
        public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    }
}
