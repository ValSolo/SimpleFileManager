using System;

namespace FileManagerSolution.Implementaion
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
