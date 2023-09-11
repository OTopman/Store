using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;
public class Book
{
    [Key]
    public int Id { get; set; }

    public required string BookUuid { get; set; }

    public required string Title { get; set; }

    public required string Author { get; set; }

    public decimal Price { get; set; }

    public int YearPublished { get; set; }

    public string? Description { get; set; }

    public required string Thumbnail { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}