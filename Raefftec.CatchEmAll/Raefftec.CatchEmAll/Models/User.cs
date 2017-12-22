namespace Raefftec.CatchEmAll.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
