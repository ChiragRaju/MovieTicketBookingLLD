﻿using MovieTicketBookingSystem;
using MovieTicketBookingSystem.ENUM;
using MovieTicketBookingSystem.Models;
using System.ComponentModel.DataAnnotations;
namespace MovieTicketBookingSystem;
public class Program
{
    public static void Main(string[] args)
    {
        var bookingSystem = MovieTicketBookingSystem.Instance;

        //add movies
        var movie1 = new Movie("M1", "Movie 1", "Description 1", 120);
        var movie2 = new Movie("M2", "Movie 2", "Description 2", 135);

        bookingSystem.AddMovie(movie1);
        bookingSystem.AddMovie(movie2);

        //add theaters
        var theater1 = new Theater("T1", "Theater 1", "Location 1", new List<Show>());
        var theater2 = new Theater("T2", "Theater 2", "Location 2", new List<Show>());

        //add shows
        var show1 = new Show("S1", movie1, theater1, DateTime.Now, DateTime.Now.AddMinutes(movie1.Duration), CreateSeats(10, 10));
        var show2 = new Show("S2", movie2, theater2, DateTime.Now, DateTime.Now.AddMinutes(movie2.Duration), CreateSeats(8, 8));
        bookingSystem.AddShow(show1);
        bookingSystem.AddShow(show2);




        //Book Tickets
        var user = new User("U1", "John Doe", "john@example.com");
        var selectedSeats = new List<Seat> { show1.Seats["1-5"], show1.Seats["1-6"] };
        var booking = bookingSystem.BookTickets(user, show1, selectedSeats);
        if (booking != null)
        {
            Console.WriteLine($"Booking successful. Booking ID: {booking.Id}");
            bookingSystem.ConfirmBooking(booking.Id);
        }
        else
        {
            Console.WriteLine("Booking failed. Seats not available.");
        }

        // Cancel booking
        bookingSystem.CancelBooking(booking.Id);
        Console.WriteLine($"Booking canceled. Booking ID: {booking.Id}");



    }
    private static Dictionary<string, Seat> CreateSeats(int rows, int columns)
    {
        var seats = new Dictionary<string, Seat>();
        for (var row = 1; row <= rows; row++)
        {
            for (var col = 1; col <= columns; col++)
            {
                var seatId = $"{row}-{col}";
                var seatType = row <= 2 ? SeatType.PREMIUM : SeatType.NORMAL;
                var price = seatType == SeatType.PREMIUM ? 150.0 : 100.0;
                var seat = new Seat(seatId, row, col, SeatStatus.AVAILABLE, seatType, price);
                seats[seatId] = seat;
            }
        }
        return seats;
    }
}