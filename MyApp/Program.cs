using Infrastructure.Services;
var movieService = new MovieService();
var screeningService = new ScreeningService();
var ticketService = new TicketService();
var theaterService = new TheaterService();
//Task1
/*
var allMovies = movieService.GetAllMovies();
var comedyMovies = allMovies.Where(m => m.Genre == "Drama").ToList();
foreach (var movie in comedyMovies)
{
    Console.WriteLine($"ID: {movie.Id} Name: {movie.Title}Director: {movie.Director}");
}
*/
//Task-2
/*
var allMovies = movieService.GetAllMovies();
var uniqueDirectors = allMovies
    .Select(m => m.Director)
    .Distinct()
    .ToList();
foreach (var director in uniqueDirectors)
{
    Console.WriteLine(director);
}
*/
//task-3
/*
var screenings = screeningService.GetAllScreenings();
var sortedScreenings = screenings.OrderBy(s => s.ScreeningTime).ToList();
foreach (var s in sortedScreenings)
{
    Console.WriteLine($"Date: {s.ScreeningTime:dd.MM HH:mm}  film: {s.MovieId} | price: {s.TicketPrice}");
}
*/
//taskk-4
/*
var allMovies = movieService.GetAllMovies();

var sortedMovies = allMovies.OrderByDescending(m => m.Year).ToList();

foreach (var movie in sortedMovies)
{
    Console.WriteLine($"Year: {movie.Year} | Title: {movie.Title} | Director: {movie.Director}");
}
*/
//task-5
/*
var screenings = screeningService.GetAllScreenings();

var top5Screenings = screenings.Take(5).ToList();

foreach (var s in top5Screenings)
{
    Console.WriteLine($"Date: {s.ScreeningTime:dd.MM HH:mm} | Film ID: {s.MovieId} | Price: {s.TicketPrice}");
}
*/
//task-6
/*
var screenings = screeningService.GetAllScreenings();
var screeningCounts = screenings.GroupBy(s => s.MovieId)
.Select(group => new 
    { 
        MovieId = group.Key, 
        Count = group.Count() 
    });

foreach (var item in screeningCounts)
{
    Console.WriteLine($"Film ID: {item.MovieId} | Screenings: {item.Count}");
}
*/
//task-7

//task-8
/*
var ticketService = new TicketService();
var allMovies = movieService.GetAllMovies();
var allScreenings = screeningService.GetAllScreenings();
var allTickets = ticketService.GetAllTickets();

foreach (var movie in allMovies)
{
    var movieScreeningIds = allScreenings.Where(s => s.MovieId == movie.Id).Select(s => s.Id);
    int count = allTickets.Count(t => movieScreeningIds.Contains(t.ScreeningId));

    Console.WriteLine($"{movie.Title}: {count} sold");
}
*/
//task-9
/*
int targetMovieId = 1;

var screenings = screeningService.GetAllScreenings();
var tickets = ticketService.GetAllTickets();

var result = tickets.Where(t => screenings.Any(s => s.Id == t.ScreeningId && s.MovieId == targetMovieId));

foreach (var t in result)
{
    Console.WriteLine($"Ticket: {t.Id} | Name: {t.CustomerName} | Seat: {t.SeatNumber}");
}
*/
//task-10
/*
var movies = movieService.GetAllMovies();
var screenings = screeningService.GetAllScreenings();
var theaters = theaterService.GetAllTheaters();

var result = screenings
    .Join(movies, s => s.MovieId, m => m.Id, (s, m) => new { s, m })
    .Join(theaters, combined => combined.s.TheaterId, t => t.Id, (combined, t) => new
    {
        MovieTitle = combined.m.Title,
        ScreeningTime = combined.s.ScreeningTime,
        TheaterName = t.Name
    });

foreach (var item in result)
{
    Console.WriteLine($"{item.MovieTitle}|{item.ScreeningTime:dd.MM HH:mm} | {item.TheaterName}");
}
*/


