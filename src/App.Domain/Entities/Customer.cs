using System;
using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class Customer : Entity<Guid>
    {
        public Customer()
        {
            
        }

        //public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public DateTime BirthDate { get; set; }
    }
}
