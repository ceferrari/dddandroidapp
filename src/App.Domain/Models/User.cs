using System;
using App.Domain.Core.Models;

namespace App.Domain.Models
{
    public class User : Entity
    {
        public User()
        {
            
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
