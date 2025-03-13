namespace auth_session.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}