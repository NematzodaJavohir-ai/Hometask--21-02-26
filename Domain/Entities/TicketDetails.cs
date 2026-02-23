using System;

namespace Domain.Entities;

public class TicketDetails
{
public int TicketId { get; set; }
    public string MovieTitle { get; set; }=string.Empty;
    public DateTime ScreeningTime { get; set; }
}
