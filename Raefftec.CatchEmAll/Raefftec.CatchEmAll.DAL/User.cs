﻿using System.ComponentModel.DataAnnotations;

namespace Raefftec.CatchEmAll.DAL
{
    public class User : IHasIdentifier
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsEnabled { get; set; }
    }
}
