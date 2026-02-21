using System;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class TheaterService:ITheaterService
{
   private const string connectionString =
   @"Host=localhost;Port=5432;
    Username=postgres;Database=theatre_db;Password=18122006";
    public int AddTheatre(Theatre theatre)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlInsert = @"INSERT INTO theaters 
                          (name, location, manager, phone, capacity) 
                          VALUES 
                          (@name, @location, @manager, @phone, @capacity)";

        using (var command = new NpgsqlCommand(sqlInsert, connection))
        {
            command.Parameters.AddWithValue("name", theatre.Name);
            command.Parameters.AddWithValue("location", theatre.Location);
            command.Parameters.AddWithValue("manager", theatre.Manager);
            command.Parameters.AddWithValue("phone", theatre.Phone);
            command.Parameters.AddWithValue("capacity", theatre.Capacity);

            return command.ExecuteNonQuery();
        }
    }
}

public List<Theatre>GetAllTheaters()
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlSelect = "SELECT * FROM theaters";

        using (var command = new NpgsqlCommand(sqlSelect, connection))
        {
            var theatres = new List<Theatre>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    theatres.Add(new Theatre
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Capacity = reader.GetInt32(5)
                    });
                }
                return theatres;
            }
        }
    }
}

public Theatre?GetTheaterById(int id)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlSelect = "SELECT * FROM theaters WHERE id = @id";

        using (var command = new NpgsqlCommand(sqlSelect, connection))
        {
            command.Parameters.AddWithValue("id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Theatre
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Capacity = reader.GetInt32(5)
                    };
                }
                return null;
            }
        }
    }
}

public int UpdateTheatre(Theatre theatre)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var sqlUpdate = @"UPDATE theaters 
                          SET name=@name, location=@location, manager=@manager, 
                              phone=@phone, capacity=@capacity 
                          WHERE id = @id";

        using (var command = new NpgsqlCommand(sqlUpdate, connection))
        {
            command.Parameters.AddWithValue("id", theatre.Id);
            command.Parameters.AddWithValue("name", theatre.Name);
            command.Parameters.AddWithValue("location", theatre.Location);
            command.Parameters.AddWithValue("manager", theatre.Manager);
            command.Parameters.AddWithValue("phone", theatre.Phone);
            command.Parameters.AddWithValue("capacity", theatre.Capacity);

            return command.ExecuteNonQuery();
        }
    }
}

public int DeleteTheatre(int id)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        var sqlDelete = "DELETE FROM theaters WHERE id = @id";
        using (var command = new NpgsqlCommand(sqlDelete, connection))
        {
            command.Parameters.AddWithValue("id", id);
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
}

   