using System;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private const string connectionString =
   @"Host=localhost;Port=5432;
    Username=postgres;Database=theatre_db;Password=18122006";
    public int AddMovie(Movie movie)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            var sqlInsert = @"INSERT INTO movies 
                          (title, director, year, duration, genre, description) 
                          VALUES 
                          (@title, @director, @year, @duration, @genre, @description)";

            using (var insertCommand = new NpgsqlCommand(sqlInsert, connection))
            {
                insertCommand.Parameters.AddWithValue("title", movie.Title);
                insertCommand.Parameters.AddWithValue("director", movie.Director);
                insertCommand.Parameters.AddWithValue("year", movie.Year);
                insertCommand.Parameters.AddWithValue("duration", movie.Duration);
                insertCommand.Parameters.AddWithValue("genre", movie.Genre);
                insertCommand.Parameters.AddWithValue("description", movie.Description);

                var result = insertCommand.ExecuteNonQuery();
                return result;
            }
        }
    }


    public int DeleteMovie(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var sqlDelete = "DELETE FROM movies WHERE id = @id";
            using (var deleteCommand = new NpgsqlCommand(sqlDelete, connection))
            {
                deleteCommand.Parameters.AddWithValue("id", id);
                connection.Open();
                return deleteCommand.ExecuteNonQuery();
            }
        }
    }

    public List<Movie> GetAllMovies()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlSelect = "SELECT * FROM movies";

            using (var selectCommand = new NpgsqlCommand(sqlSelect, connection))
            {
                var movies = new List<Movie>();
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Director = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Duration = reader.GetInt32(4),
                            Genre = reader.GetString(5),
                            Description = reader.GetString(6)
                        });
                    }
                    return movies;
                }
            }
        }
    }

    public Movie? GetMovieById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlSelect = "SELECT * FROM movies WHERE id = @id";

            using (var selectCommand = new NpgsqlCommand(sqlSelect, connection))
            {
                selectCommand.Parameters.AddWithValue("id", id);

                using (var reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Director = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Duration = reader.GetInt32(4),
                            Genre = reader.GetString(5),
                            Description = reader.GetString(6)
                        };
                    }
                    return null;
                }
            }
        }
    }

    public int UpdateMovie(Movie movie)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlUpdate = @"UPDATE movies 
                          SET title=@title, director=@director, year=@year, 
                              duration=@duration, genre=@genre, description=@description 
                          WHERE id = @id";

            using (var updateCommand = new NpgsqlCommand(sqlUpdate, connection))
            {
                updateCommand.Parameters.AddWithValue("id", movie.Id);
                updateCommand.Parameters.AddWithValue("title", movie.Title);
                updateCommand.Parameters.AddWithValue("director", movie.Director);
                updateCommand.Parameters.AddWithValue("year", movie.Year);
                updateCommand.Parameters.AddWithValue("duration", movie.Duration);
                updateCommand.Parameters.AddWithValue("genre", movie.Genre);
                updateCommand.Parameters.AddWithValue("description", movie.Description);

                return updateCommand.ExecuteNonQuery();
            }
        }
    }
}