using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Exceptions;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        public CreateBookingCommandHandler()
        {
        }

        public Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (FakeDbStorage.BookingModels.Any(_ => request.BookingTimeConverted >= _.BookingTime
                                                     && request.BookingTimeConverted <=
                                                     _.BookingTime + TimeSpan.FromMinutes(59)))
                throw new ConflictException("Booking is reserved!");
            var result = new BookingDto();
            FakeDbStorage.BookingModels.Add(new()
            {
                Id = result.BookingId,
                BookingName = request.Name,
                BookingTime = request.BookingTimeConverted
            });
            return Task.FromResult(result);
        }
    }
}