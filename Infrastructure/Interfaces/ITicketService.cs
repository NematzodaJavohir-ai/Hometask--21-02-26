using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITicketService
{
int AddTicket(Ticket ticket);
List<Ticket> GetAllTickets();
Ticket? GetTicketById(int id);
int UpdateTicket(Ticket ticket);
int DeleteTicket(int id);
List<TicketswithTheatrs>GetTicketandTheatres(string thname);

}
