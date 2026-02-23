using System;
using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;
public class MovieService : IMovieService
{
  private const string connectionString = @"Host=localhost;Port=5432;Username=postgres;Database=theatre_db;Password=18122006";
    public int AddMovie(Movie movie)
    {
        using(var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlInsert = @"Insert into movies(title, director, year, duration, genre, description 
            values(@title, @director, @year, @duration, @genre, @description )";

            using(var commandInsert = new NpgsqlCommand(sqlInsert,connection))
            {
                commandInsert.Parameters.AddWithValue("title",movie.Title);
                commandInsert.Parameters.AddWithValue("director",movie.Director);
                commandInsert.Parameters.AddWithValue("year",movie.Year);
                commandInsert.Parameters.AddWithValue("duration",movie.Duration);
                commandInsert.Parameters.AddWithValue(" genre",movie.Genre);
                commandInsert.Parameters.AddWithValue(" description ",movie.Description);
                int result = commandInsert.ExecuteNonQuery();
                return result;
                
            }

        }
    }
    public int DeleteMovie(int Id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            var sqlDelete = "Delete from Movies where id =@id";

            using(var deleteCommand = new NpgsqlCommand(sqlDelete, connection))
            {
                deleteCommand.Parameters.AddWithValue("id",Id);
                return deleteCommand.ExecuteNonQuery();
            }
        }
    }
    public List<Movie> GetAllMovies()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlselect = @"select * from Movies";

            using(var selectCommand = new NpgsqlCommand(sqlselect, connection)) 
            {
                 var movies = new List<Movie>();
                 using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            movies.Add(new Movie{
                            Id = reader.GetInt32(0),
                            Title=reader.GetString(1),
                            Director=reader.GetString(2),
                            Year= reader.GetInt32(3),
                            Duration=reader.GetInt32(4),
                            Genre=reader.GetString(5),
                            Description= reader.GetString(6)
                            
                        });
                       
                    }
                     return movies;
                }
            }
            
        }
    }
    public Movie? GetMovieById(int Id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
              var sqlselect = @"select * from Movies where id = @id";

            using(var selectCommand = new NpgsqlCommand(sqlselect, connection))
            {
                selectCommand.Parameters.AddWithValue("id",Id);
                using(var reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title=reader.GetString(1),
                            Director=reader.GetString(2),
                            Year= reader.GetInt32(3),
                            Duration=reader.GetInt32(4),
                            Genre=reader.GetString(5),
                            Description= reader.GetString(6)
 
                        };
                    }
                    return null;
                }
            }
            
        }
    }

    public List<Movie> GetMoviesByGenre(string genre)
    {
        var movies = new List<Movie>();
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        const string sql = "Select * from Movies where genre ilike @genre";
        using var command = new NpgsqlCommand(sql,connection);
        command.Parameters.AddWithValue("genre",genre);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
             movies.Add(new Movie
        { 
            Id=reader.GetInt32(0),
            Title=reader.GetString(1),
             Director= reader.GetString(2),
             Year= reader.GetInt32(3),
             Duration=reader.GetInt32(4),
             Genre=reader.GetString(5),
             Description=reader.GetString(6)
            
        });
        }
        return movies;
    }

    public List<Movie> GetMoviesOrderByYear()
    {
        var movies = new List<Movie>();
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        const string sql = "Select * from Movies order by year desc";
        using var command = new NpgsqlCommand(sql,connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
             movies.Add(new Movie
        { 
            Id=reader.GetInt32(0),
            Title=reader.GetString(1),
             Director= reader.GetString(2),
             Year= reader.GetInt32(3),
             Duration=reader.GetInt32(4),
             Genre=reader.GetString(5),
             Description=reader.GetString(6)
            
        });
        }
        return movies;
    }

    public List<MovieScreeningCount> GetScreeningCountsMovie()
    {
        var movies = new List<MovieScreeningCount>();
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
      
        const string sql = "Select movie_id,count(id)from screenings group by movie_id";
        using var command = new NpgsqlCommand(sql,connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            movies.Add(new MovieScreeningCount
            {
                MovieId=reader.GetInt32(0),
                ScreeningCount=(int)reader.GetInt64(1)
            });
        }
        return movies;

    }

    public List<string> GetUniqueDirectorsByYears(int[] years)
    {
        var directors = new List<string>();
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        const string sql = "Select distinct director from movies where year = any(@year) order by director";
        using var command = new NpgsqlCommand(sql,connection);
        command.Parameters.AddWithValue("year",years);
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            if (!reader.IsDBNull(0))
            {
                
            directors.Add(reader.GetString(0));
            }
        }
        return directors;

    }

    public int UpdateMovie(Movie movie)
    {
         using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sqlUpdate = @"update movies set title=@title, director=@director, year=@year, 
                              duration=@duration, genre=@genre,description=@description
                          WHERE id = @id";
            using(var updateCommand = new NpgsqlCommand(sqlUpdate, connection))
            {
                updateCommand.Parameters.AddWithValue("id",movie.Id);
                updateCommand.Parameters.AddWithValue("title",movie.Title);
                updateCommand.Parameters.AddWithValue("director",movie.Director);
                updateCommand.Parameters.AddWithValue("year",movie.Year);
                updateCommand.Parameters.AddWithValue("duration",movie.Duration);
                updateCommand.Parameters.AddWithValue("genre",movie.Genre);
                updateCommand.Parameters.AddWithValue("description",movie.Description);
                var result = updateCommand.ExecuteNonQuery();
                return result;
            }
        }
    }
    public List<MovieTickets> GetTotalTicketsPerMovies()
{
    var stats = new List<MovieTickets>();
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    const string sql = @"
        SELECT m.title, COUNT(t.id) 
        FROM movies m
        JOIN screenings s ON m.id = s.movie_id
        JOIN tickets t ON s.id = t.screening_id
        GROUP BY m.title";

    using var command = new NpgsqlCommand(sql, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        stats.Add(new MovieTickets
        {
            MovieTitle = reader.GetString(0),
            
            TotalTickets = (int)reader.GetInt64(1) 
        });
    }
    return stats;
}
public List<TicketDetails> GetTicketsByMovieTitle(string movieTitle)
{
    var tickets = new List<TicketDetails>();
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    const string sql = @"
        SELECT t.id, m.title, s.screening_time
        FROM tickets t
        JOIN screenings s ON t.screening_id = s.id
        JOIN movies m ON s.movie_id = m.id
        WHERE m.title ILIKE @title";

    using var command = new NpgsqlCommand(sql, connection);
    command.Parameters.AddWithValue("title", movieTitle);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        tickets.Add(new TicketDetails
        {
            TicketId = reader.GetInt32(0),
            MovieTitle = reader.GetString(1),
            ScreeningTime = reader.GetDateTime(2)
        });
    }
    return tickets;
}
public List<ScreeningInfo> GetFullScreeningDetails()
{
    var list = new List<ScreeningInfo>();
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    const string sql = @"
        SELECT m.title, s.screening_time, t.name
        FROM screenings s
        JOIN movies m ON s.movie_id = m.id
        JOIN theaters t ON s.theater_id = t.id";

    using var command = new NpgsqlCommand(sql, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        list.Add(new ScreeningInfo
        {
            MovieTitle = reader.GetString(0),
            ScreeningTime = reader.GetDateTime(1),
            TheaterName = reader.GetString(2)
        });
    }
    return list;
}
}
