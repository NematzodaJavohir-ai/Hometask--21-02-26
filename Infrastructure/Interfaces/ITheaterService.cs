using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITheaterService
{
   int  AddTheatre(Theatre theatre);
  List<Theatre> GetAllTheaters();
  Theatre? GetTheaterById(int Id);
  int UpdateTheatre(Theatre theatre );
  int DeleteTheatre(int Id);
}
