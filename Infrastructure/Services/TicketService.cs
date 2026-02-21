using System;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class TicketService:ITicketService
{
  private const string connectionString =
   @"Host=localhost;Port=5432;
    Username=postgres;Database=theatre_db;Password=18122006";
    public int AddTicket(Ticket ticket)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlInsert = @"INSERT INTO tickets 
                          (screening_id, customer_name, seat_number, price) 
                          VALUES 
                          (@screening_id, @customer_name, @seat_number, @price)";

        using (var command = new NpgsqlCommand(sqlInsert, connection))
        {
            command.Parameters.AddWithValue("screening_id", ticket.ScreeningId);
            command.Parameters.AddWithValue("customer_name", ticket.CustomerName);
            command.Parameters.AddWithValue("seat_number", ticket.SeatNumber);
            command.Parameters.AddWithValue("price", ticket.Price);

            return command.ExecuteNonQuery();
        }
    }
}

public List<Ticket> GetAllTickets()
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlSelect = "SELECT * FROM tickets";

        using (var command = new NpgsqlCommand(sqlSelect, connection))
        {
            var tickets = new List<Ticket>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tickets.Add(new Ticket
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetDecimal(4)
                    });
                }
                return tickets;
            }
        }
    }
}

public Ticket? GetTicketById(int id)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlSelect = "SELECT * FROM tickets WHERE id = @id";

        using (var command = new NpgsqlCommand(sqlSelect, connection))
        {
            command.Parameters.AddWithValue("id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Ticket
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetDecimal(4)
                    };
                }
                return null;
            }
        }
    }
}

public int UpdateTicket(Ticket ticket)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlUpdate = @"UPDATE tickets 
                          SET screening_id=@screening_id, customer_name=@customer_name, 
                              seat_number=@seat_number, price=@price 
                          WHERE id = @id";

        using (var command = new NpgsqlCommand(sqlUpdate, connection))
        {
            command.Parameters.AddWithValue("id", ticket.Id);
            command.Parameters.AddWithValue("screening_id", ticket.ScreeningId);
            command.Parameters.AddWithValue("customer_name", ticket.CustomerName);
            command.Parameters.AddWithValue("seat_number", ticket.SeatNumber);
            command.Parameters.AddWithValue("price", ticket.Price);

            return command.ExecuteNonQuery();
        }
    }
}

public int DeleteTicket(int id)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        var sqlDelete = "DELETE FROM tickets WHERE id = @id";
        using (var command = new NpgsqlCommand(sqlDelete, connection))
        {
            command.Parameters.AddWithValue("id", id);
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}
