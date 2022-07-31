using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        public CreateBookingCommandHandler()
        {
        }

        public Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // TODO Implement CreateBookingCommandHandler.Handle() and confirm tests are passing. See InfoTrack Global Team - Tech Test.pdf for business rules.
            throw new NotImplementedException();
        }
    }
}
