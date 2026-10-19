using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<BookLocation> BookLocations { get; set; } = new List<BookLocation>();
}
