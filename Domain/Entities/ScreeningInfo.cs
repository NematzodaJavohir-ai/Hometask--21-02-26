using System;

namespace Domain.Entities;

public class ScreeningInfo
{
public string MovieTitle { get; set; }=string.Empty;
    public DateTime ScreeningTime { get; set; }
    public string TheaterName { get; set; }=string.Empty;
}
