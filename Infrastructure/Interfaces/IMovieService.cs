using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IMovieService
{
  int AddMovie(Movie movie);
  List<Movie> GetAllMovies();
  Movie? GetMovieById(int Id);
  int UpdateMovie(Movie movie);
  int DeleteMovie(int Id);
  List<Movie> GetMoviesByGenre(string genre);
  List<Movie> GetMoviesOrderByYear();
  List<string>GetUniqueDirectorsByYears(int[] years);
  List<MovieScreeningCount> GetScreeningCountsMovie();
  List<MovieTickets>GetTotalTicketsPerMovies();
  List<TicketDetails> GetTicketsByMovieTitle(string movieTitle);
   List<ScreeningInfo> GetFullScreeningDetails();
}
