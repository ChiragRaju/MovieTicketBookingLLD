using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBookingSystem.Models
{
    public class User
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; }

        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

    }
}
