namespace WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string PwdHash { get; set; } = null!;
        public string PwdSalt {get; set; }  = null!;

    public bool IsAdming {  get; set; }

    
    }
}
