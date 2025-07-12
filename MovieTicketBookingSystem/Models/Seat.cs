using MovieTicketBookingSystem.ENUM;

namespace MovieTicketBookingSystem.Models
{
    public class Seat
    {
        public string Id { get; }
        public int Row { get; }
        public int Column { get; }
        public SeatStatus Status { get; set; }
        public SeatType SeatType { get; }
        public double Price { get; }

        public Seat(string id, int row, int column, SeatStatus status, SeatType type, double price)
        {
            Id = id;
            Row = row;
            Column = column;
            Status = status;
            SeatType = type;
            Price = price;
        }
    }
}