using MovieTicketBookingSystem.ENUM;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Utilities;
using System.Collections.Concurrent;

namespace MovieTicketBookingSystem
{
    public class MovieTicketBookingSystem
    {
        //singleton instance
        private static MovieTicketBookingSystem _instance;
        private static readonly object _lock = new object();

        private readonly List<Movie> _movies;
        private readonly List<Theater> _theaters;
        private readonly ConcurrentDictionary<string, Show> _shows;
        private readonly ConcurrentDictionary<string, Booking> _bookings;


        public MovieTicketBookingSystem()
        {
            _movies = new List<Movie>();
            _theaters = new List<Theater>();
            _shows = new ConcurrentDictionary<string, Show>();
            _bookings = new ConcurrentDictionary<string, Booking>();
        }
        public static MovieTicketBookingSystem Instance
        {
            get
            {
                lock (_lock)
                {
                    {
                        if (_instance == null)

                            _instance = new MovieTicketBookingSystem();
                    }
                    return _instance;
                }
            }
        }

        public void AddMovie(Movie Movie)
        {
            _movies.Add(Movie);

        }
        public void AddTheater(Theater theater)
        {
            _theaters.Add(theater);
        }
        public void AddShow(Show show)
        {
            _shows[show.Id] = show;
        }

        public Show GetShows(string showId)

        {
            _shows.TryGetValue(showId, out var show);
            return show;
        }

        public List<Theater> GetTheaters() => _theaters;

        public List<Movie> GetMovies() => _movies;

        private bool AreSeatsAvailable(Show show, List<Seat> Selectedseats)
        {
            return Selectedseats.All(seat => show.Seats.ContainsKey(seat.Id) && show.Seats[seat.Id].Status == SeatStatus.AVAILABLE);

        }
        private void MarkSeatsAsBooked(Show show, List<Seat> selectedSeats)
        {
            foreach (var seat in selectedSeats)
            {
                show.Seats[seat.Id].Status = SeatStatus.BOOKED;
            }
        }
        private void MarkSeatAsAvailable(Show show, List<Seat> selectedSeats)
        {
            foreach (var seat in selectedSeats)
            {
                show.Seats[seat.Id].Status = SeatStatus.AVAILABLE;
            }
        }
        private double CalculateTotalPrice(List<Seat> selectedSeats)
        {
            return selectedSeats.Sum(seat => seat.Price);
        }

        public Booking BookTickets(User user, Show show, List<Seat> Selectedseats)
        {
            lock (this)
            {
                if (AreSeatsAvailable(show, Selectedseats))
                {
                    MarkSeatsAsBooked(show, Selectedseats);
                    var totalPrice = CalculateTotalPrice(Selectedseats);
                    var bookingId = BookingUtils.GenerateBookingId();

                    var booking = new Booking(bookingId, user, show, Selectedseats, totalPrice, BookingStatus.PENDING);
                    return booking;
                }

                return null;
            }


        }

        public void ConfirmBooking(string bookingId)
        {
            lock (this)
            {
                if (_bookings.TryGetValue(bookingId, out var booking) && booking.Status == BookingStatus.PENDING)
                {
                    booking.Status = BookingStatus.CONFIRMED;
                }
            }
        }

        public void CancelBooking(string bookingId)
        {
            lock (this)
            {
                if (_bookings.TryGetValue(bookingId, out var booking) && booking.Status != BookingStatus.CANCELLED)
                {
                    booking.Status = BookingStatus.CANCELLED;
                    MarkSeatAsAvailable(booking.Show, booking.Seats);
                }
            }
        }


    }
}
