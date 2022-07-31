using System;
using System.Globalization;
using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<BookingDto>
    {
        public string Name { get; set; }
        public string BookingTime { get; set; }
        public TimeSpan BookingTimeConverted => ConvertBookingTime(BookingTime);

        private TimeSpan ConvertBookingTime(string datetime)
        {
            var result = TimeSpan.Parse(datetime);
            return result;
        }
    }
}