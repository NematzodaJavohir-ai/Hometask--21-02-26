using System.ComponentModel.Design;
using Infrastructure.Services;
var movieService = new MovieService();
var screeningService = new ScreeningService();
var ticketService = new TicketService();
var theaterService = new TheaterService();
//task--1
foreach (var item in movieService.GetMoviesByGenre("Drama"))
{
    Console.WriteLine(item.Title);
    Console.WriteLine(item.Director);
    Console.WriteLine(item.Year);
    Console.WriteLine(item.Genre);
    Console.WriteLine(item.Description);
    Console.WriteLine("__________________________");
}
//task--2
int[] years = {2019,2021,2020};
var directors=movieService.GetUniqueDirectorsByYears(years);
foreach (var item in directors)
{
    Console.WriteLine(item);
    Console.WriteLine();
}
//task--3
foreach (var item in screeningService.GetAllScreeningsOrderByScreeningtime())
{
    Console.WriteLine(item.MovieId);
    Console.WriteLine(item.ScreeningRoom);
    Console.WriteLine(item.TicketPrice);
    Console.WriteLine(item.TheaterId);
    Console.WriteLine(item.ScreeningTime);
    Console.WriteLine("_______________________");
}
//task-4
foreach (var item in movieService.GetMoviesOrderByYear())
{
    Console.WriteLine(item.Title);
    Console.WriteLine(item.Director);
    Console.WriteLine(item.Year);
    Console.WriteLine(item.Genre);
    Console.WriteLine(item.Description);
    Console.WriteLine("__________________________");
}
//task-5
foreach (var item in screeningService.GetFirstFiveScreenings())
{
    Console.WriteLine(item.MovieId);
    Console.WriteLine(item.ScreeningRoom);
    Console.WriteLine(item.TicketPrice);
    Console.WriteLine(item.TheaterId);
    Console.WriteLine(item.ScreeningTime);
    Console.WriteLine("_______________________");
}
//task-6
var screenings = movieService.GetScreeningCountsMovie();
if (screenings.Count == 0)
{
    Console.WriteLine("seansi ne naydeni");
}
else
{
    foreach (var item in screenings)
    {
        Console.WriteLine($"Movie_id:{item.MovieId}|Kol_seansov:{item.ScreeningCount}");
        Console.WriteLine();
    }
}
//task-7
Console.Write("napishite imya kinotetra:");
string a = Console.ReadLine();

foreach (var item in ticketService.GetTicketandTheatres(a))
{
    Console.WriteLine($"Bilet nomer:{item.ticket_id}|kinoteatr:{item.theater_name}");
    Console.WriteLine();
}
//task-8
var ticketcount = movieService.GetTotalTicketsPerMovies();

foreach (var item in ticketcount)
{
    Console.WriteLine($"film: {item.MovieTitle} | Tickets sold: {item.TotalTickets}");
     Console.WriteLine();
}
// task-9
Console.Write("Vvedite nazvanie filma: ");
string movieName = Console.ReadLine();

var tickets = movieService.GetTicketsByMovieTitle(movieName);

foreach (var item in tickets)
{
    Console.WriteLine($"Ticket ID: {item.TicketId} | Movie: {item.MovieTitle} | Time: {item.ScreeningTime}");
    Console.WriteLine();
}
// task-10
var fullDetails = movieService.GetFullScreeningDetails();

foreach (var item in fullDetails)
{
    Console.WriteLine($"Movie: {item.MovieTitle} | Time: {item.ScreeningTime} | Theater: {item.TheaterName}");
    Console.WriteLine();
}


