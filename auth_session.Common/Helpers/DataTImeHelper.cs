namespace auth_session.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetCurrentDateTimeFormatted()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}