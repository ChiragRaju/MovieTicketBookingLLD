using System;
using System.Threading;

namespace MovieTicketBookingSystem.Utilities
{
    public static class BookingUtils
    {
        private static readonly string Booking_ID_PREFIX = "BKG";
        private static long _bookingCounter = 0;

        public static string GenerateBookingId()
        {
            var bookingNumber = Interlocked.Increment(ref _bookingCounter);
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{Booking_ID_PREFIX}{timestamp}{bookingNumber:D6}";
        }
    }
}
