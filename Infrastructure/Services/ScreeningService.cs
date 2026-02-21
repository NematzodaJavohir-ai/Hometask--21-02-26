using System;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class ScreeningService : IScreeningService
{
    private const string connectionString =
   @"Host=localhost;Port=5432;
    Username=postgres;Database=theatre_db;Password=18122006";
    public int AddScreening(Screening screening)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlInsert = @"INSERT INTO screenings 
                          (movie_id, theater_id, screening_time, ticket_price, available_seats) 
                          VALUES 
                          (@movie_id, @theater_id, @screening_time, @ticket_price, @available_seats)";

            using (var command = new NpgsqlCommand(sqlInsert, connection))
            {
                command.Parameters.AddWithValue("movie_id", screening.MovieId);
                command.Parameters.AddWithValue("theater_id", screening.TheaterId);
                command.Parameters.AddWithValue("screening_time", screening.ScreeningTime);
                command.Parameters.AddWithValue("ticket_price", screening.TicketPrice);
                command.Parameters.AddWithValue("available_seats", screening.AvailableSeats);

                return command.ExecuteNonQuery();
            }
        }
    }

    public List<Screening> GetAllScreenings()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlSelect = "SELECT * FROM screenings";

            using (var command = new NpgsqlCommand(sqlSelect, connection))
            {
                var screenings = new List<Screening>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        screenings.Add(new Screening
                        {
                            Id = reader.GetInt32(0),
                            MovieId = reader.GetInt32(1),
                            TheaterId = reader.GetInt32(2),
                            ScreeningTime = reader.GetDateTime(3),
                            TicketPrice = reader.GetDecimal(4),
                            AvailableSeats = reader.GetInt32(5)
                        });
                    }
                    return screenings;
                }
            }
        }
    }

    public Screening? GetScreeningById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlSelect = "SELECT * FROM screenings WHERE id = @id";

            using (var command = new NpgsqlCommand(sqlSelect, connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Screening
                        {
                            Id = reader.GetInt32(0),
                            MovieId = reader.GetInt32(1),
                            TheaterId = reader.GetInt32(2),
                            ScreeningTime = reader.GetDateTime(3),
                            TicketPrice = reader.GetDecimal(4),
                            AvailableSeats = reader.GetInt32(5)
                        };
                    }
                    return null;
                }
            }
        }
    }

    public int UpdateScreening(Screening screening)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlUpdate = @"UPDATE screenings 
                          SET movie_id=@movie_id, theater_id=@theater_id, 
                              screening_time=@screening_time, ticket_price=@ticket_price, 
                              available_seats=@available_seats 
                          WHERE id = @id";

            using (var command = new NpgsqlCommand(sqlUpdate, connection))
            {
                command.Parameters.AddWithValue("id", screening.Id);
                command.Parameters.AddWithValue("movie_id", screening.MovieId);
                command.Parameters.AddWithValue("theater_id", screening.TheaterId);
                command.Parameters.AddWithValue("screening_time", screening.ScreeningTime);
                command.Parameters.AddWithValue("ticket_price", screening.TicketPrice);
                command.Parameters.AddWithValue("available_seats", screening.AvailableSeats);

                return command.ExecuteNonQuery();
            }
        }
    }

    public int DeleteScreening(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sqlDelete = "DELETE FROM screenings WHERE id = @id";
            using (var command = new NpgsqlCommand(sqlDelete, connection))
            {
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
