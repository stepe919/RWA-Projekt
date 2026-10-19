using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Author { get; set; } = null!;

    public string? Isbn { get; set; }

    public int? PublishedYear { get; set; }

    public int GenreId { get; set; }

    public virtual ICollection<BookLocation> BookLocations { get; set; } = new List<BookLocation>();

    public virtual Genre Genre { get; set; } = null!;
}
