using System.ComponentModel.DataAnnotations;

namespace NextPage.Data;

public class BookModel
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public GenreEnum Genre { get; set; }

    public int Year { get; set; }

    public string? Description { get; set; }
}
