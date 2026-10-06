namespace WebApi.Models
{
    public class Book
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int GenreId { get; set; }
        public string Author { get; set; }  = null!;

        public string? Isbn { get; set; }
        
        public int? PublishedYear { get; set; }
    }
}
