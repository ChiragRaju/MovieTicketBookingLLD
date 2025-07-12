using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBookingSystem.Models
{
    public class Theater
    {
        public string Id { get;  }
        public string Name { get; }
        public string Location { get; }
        public List<Show> Shows { get; }

        public Theater(string id, string name, string location, List<Show> shows)
        {
            Id = id;
            Name = name;
            Location = location;
            Shows = shows;
        }

    }
}
