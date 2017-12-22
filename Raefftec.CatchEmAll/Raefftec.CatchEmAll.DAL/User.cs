using System.ComponentModel.DataAnnotations;

namespace Raefftec.CatchEmAll.DAL
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
