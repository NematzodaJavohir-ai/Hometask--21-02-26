using System;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IScreeningService
{
int AddScreening(Screening screening);
List<Screening> GetAllScreenings();
Screening? GetScreeningById(int id);
int UpdateScreening(Screening screening);
int DeleteScreening(int id);
List<Screening> GetAllScreeningsOrderByScreeningtime();
List<Screening> GetFirstFiveScreenings();

}
