using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBookingSystem.Models
{
    public class Movie
    {
        public string? MovieId { get; }
        public string? MovieName { get; }
        public string Description { get; } 
        public int Duration { get; }

        public Movie(string movieId,string movieName,string description,int duration)
        {
            MovieId = movieId;
            MovieName = movieName;
            Description = description;
            Duration = duration;
        }
    }
}
