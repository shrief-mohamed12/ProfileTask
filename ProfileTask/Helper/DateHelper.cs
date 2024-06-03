using Microsoft.EntityFrameworkCore;
using ProfileTask.Models;

namespace ProfileTask.Helper
{
    public static class DateHelper
    {
        public static string FormatDateRange(DateTime dateFrom, DateTime dateTo)
        {
            string dateFromFormatted = dateFrom.ToString("MMMM yyyy");
            bool isCurrentMonth = dateTo.Year == DateTime.Now.Year && dateTo.Month == DateTime.Now.Month;
            string dateToFormatted = isCurrentMonth ? "Present" : dateTo.ToString("MMMM yyyy");
            return $"{dateFromFormatted} - {dateToFormatted}";
        }
    }

    public class demo
    {
        private readonly ApplicationDbContext _db;
        public demo(ApplicationDbContext db)
        {
            _db= db;
        }
        public async void GenerateEmployeesAndSetTheirData()
        {
            var employees = new List<Employee>()
            {
                new Employee
                {
                    Id = 0,
                    employeeName = "Ahmed"
                },
                new Employee
                {
                    Id = 0,
                    employeeName = "Ahmed1"
                }
            };

            await _db.Employees.AddRangeAsync(employees);
            await _db.SaveChangesAsync();

            employees[0].about = "test";
            await _db.SaveChangesAsync();
        }
    }


}
