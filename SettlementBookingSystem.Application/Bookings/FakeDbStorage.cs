using System.Collections.Generic;
using SettlementBookingSystem.Application.Bookings.Dtos;

namespace SettlementBookingSystem.Application.Bookings;

public static class FakeDbStorage
{
    public static List<BookingModel> BookingModels { get; } = new();
}