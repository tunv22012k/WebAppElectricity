using System;
using System.Collections.Generic;

namespace WebAppElectricity.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        // Navigation property
        public ICollection<Category> Categories { get; set; }
    }
}