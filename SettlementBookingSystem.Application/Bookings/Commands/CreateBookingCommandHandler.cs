using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Exceptions;
using FluentValidation;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(IValidator<CreateBookingCommand> validator)
        {
            _validator = validator;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var errors = await _validator.ValidateAsync(request);

            if (errors.Errors.Any())
                throw new ValidationException("Invalid request");

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

            return result;
        }
    }
}