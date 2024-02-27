using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class Movie
{
	public Movie() {}
    public Movie(string? title, string? genre, decimal price, DateTime releaseDate, string rating)
    {
        Title = title;
        Genre = genre;
        Price = price;
        ReleaseDate = releaseDate;
        Rating = rating;
    }

    public uint Id { get; set; }
    
    [StringLength(60, MinimumLength = 3), Required]
	public string? Title { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
	public string? Genre { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
	public decimal Price { get; set; }

    [Display(Name = "Release Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Range(typeof(DateTime), "01/01/1966", "01/01/2024")]
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required]
    public string Rating { get; set; } = string.Empty;
}