using Barometr.Data;
using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class BusinessHoursService
    {
        private BusinessHoursRepository _repo;

        public BusinessHoursService (BusinessHoursRepository repo)
        {
            _repo = repo;
        }

        private string GetHoursByDay(int barId, int dayId)
        {
            return _repo.GetHoursByBarId(barId).FirstOrDefault(b => b.Day == dayId).OpenTime + " - " + _repo.GetHoursByBarId(barId).FirstOrDefault(b => b.Day == dayId).CloseTime;
        }

        public BusinessHoursDTO GetWeekHours(int barId)
        {
            return new BusinessHoursDTO
            {
                Sunday = GetHoursByDay(barId, 0),
                Monday = GetHoursByDay(barId, 1),
                Tuesday = GetHoursByDay(barId, 2),
                Wednesday = GetHoursByDay(barId, 3),
                Thursday = GetHoursByDay(barId, 4),
                Friday = GetHoursByDay(barId, 5),
                Saturday = GetHoursByDay(barId, 6)
            };
        }
    }
}
