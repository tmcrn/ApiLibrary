using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Models;

public class Book
{
    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public int AuthorId { get; set; }
    public int? ReleaseYear { get; set; }
    [Required, MaxLength(20)] public string ISBN { get; set; }
    public Author? Author { get; set; }
}

