using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IMovieService
{
int AddMovie(Movie movie);
List<Movie> GetAllMovies();
Movie? GetMovieById(int id);
int UpdateMovie(Movie movie);
int DeleteMovie(int id);
}
