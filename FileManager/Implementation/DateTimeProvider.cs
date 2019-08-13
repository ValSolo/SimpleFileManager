using System;

namespace SimpleFileManager.Implementation
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
