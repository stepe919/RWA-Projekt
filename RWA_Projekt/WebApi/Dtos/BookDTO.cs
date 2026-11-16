using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(256)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(256)]
        public string Author { get; set; } = null!;

        [StringLength(256)]
        public string? Isbn { get; set; }

        public string? Description { get; set; }

        public int? PublishedYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "GenreId is required")]
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
    }
}