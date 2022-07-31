using System;
using FluentValidation;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly TimeSpan _timeStart = new TimeSpan(9, 0, 0);
        private readonly TimeSpan _timeEnd = new TimeSpan(16, 0, 0);

        public CreateBookingValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.BookingTime).Matches("[0-9]{1,2}:[0-9][0-9]");

            RuleFor(b => b.BookingTimeConverted)
                .Must(val => val >= _timeStart && val <= _timeEnd)
                .WithMessage("Booking should be from 9:00 AM to 4:00 PM");
        }
    }
}