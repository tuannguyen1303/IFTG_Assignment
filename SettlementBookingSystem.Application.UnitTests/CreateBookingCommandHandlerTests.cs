using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using SettlementBookingSystem.Application.Bookings;
using SettlementBookingSystem.Application.Bookings.Commands;
using SettlementBookingSystem.Application.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SettlementBookingSystem.Application.UnitTests
{
    public class CreateBookingCommandHandlerTests
    {
        [Fact]
        public async Task GivenValidBookingTime_WhenNoConflictingBookings_ThenBookingIsAccepted()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "09:15",
            };

            if (FakeDbStorage.BookingModels.Any())
                FakeDbStorage.BookingModels.Clear();

            var mockValidator = new Mock<IValidator<CreateBookingCommand>>();

            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new CreateBookingCommandHandler(mockValidator.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.BookingId.Should().NotBeEmpty();
        }

        [Fact]
        public void GivenOutOfHoursBookingTime_WhenBooking_ThenValidationFails()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "00:00",
            };

            var mockValidator = new Mock<IValidator<CreateBookingCommand>>();

            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new ValidationException("Invalid request"));

            var handler = new CreateBookingCommandHandler(mockValidator.Object);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            act.Should().Throw<ValidationException>();
        }

        [Fact]
        public void GivenValidBookingTime_WhenBookingIsFull_ThenConflictThrown()
        {
            var command = new CreateBookingCommand
            {
                Name = "test",
                BookingTime = "09:15",
            };

            FakeDbStorage.BookingModels.Add(new()
            {
                Id = Guid.NewGuid(),
                BookingName = command.Name,
                BookingTime = command.BookingTimeConverted
            });

            var mockValidator = new Mock<IValidator<CreateBookingCommand>>();

            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new CreateBookingCommandHandler(mockValidator.Object);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            act.Should().Throw<ConflictException>();
        }
    }
}
