namespace WebApi.Models
{
    public class Log
    {

        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int Level { get; set; }


        public string Message { get; set; } = null;
    }
}
