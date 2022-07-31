using System;

namespace SettlementBookingSystem.Application.Bookings.Dtos;

public class BookingModel
{
    public Guid Id { get; set; }
    public string BookingName { get; set; }
    public TimeSpan BookingTime { get; set; }
}