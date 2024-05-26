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

}
