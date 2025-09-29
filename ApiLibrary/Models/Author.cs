using System.ComponentModel.DataAnnotations;

namespace ApiLibrary.Models;

public class Author
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; }
    [Required, MaxLength(100)] public string Firstname { get; set; }
    public List<Book>? Books { get; set; }
    
    public override string ToString()
    {
        return $"{Firstname} {Name}";
    }
}