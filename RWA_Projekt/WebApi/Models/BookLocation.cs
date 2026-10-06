namespace WebApi.Models
{
    public class BookLocation
    {

        public int Id { get; set; }
        public int BookId { get; set; }

        public int LocationId { get; set; }

        public int Quantity { get; set; }
    }
}
