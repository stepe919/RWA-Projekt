using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class BookLocationDto
    {

        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }
        [Range(1, int.MaxValue)]
        public int LocationId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }


    }
}