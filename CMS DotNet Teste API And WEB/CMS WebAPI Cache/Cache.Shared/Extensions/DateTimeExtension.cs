using TimeZoneConverter;

namespace Cache.Shared.Extensions;

public static class DateTimeExtension
{
    public static readonly TimeZoneInfo TimeZoneBrasilia = TZConvert.GetTimeZoneInfo("E. South America Standard Time");

    public static readonly TimeZoneInfo TimeZoneUtc = TZConvert.GetTimeZoneInfo("UTC");

    //
    // Summary:
    //     Convert a UTC datetime to Brasilia
    //
    // Parameters:
    //   dateTime:
    //     Datetime in UTC
    //
    // Returns:
    //     Datetime in Brazilia TMZ
    public static DateTime ToDtHrBrasilia(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTime(dateTime, TimeZoneBrasilia);
    }

    //
    // Summary:
    //     Convert a UTC datetime to Brasilia
    //
    // Parameters:
    //   dateTime:
    //     Datetime in UTC
    //
    // Returns:
    //     Datetime in Brazilia TMZ
    public static DateTime? ToDtHrBrasilia(this DateTime? dateTime)
    {
        return dateTime?.ToDtHrBrasilia();
    }

    //
    // Summary:
    //     Convert Brasilia datetime to UTC
    //
    // Parameters:
    //   dateTime:
    //     Datetime in Brasilia
    //
    // Returns:
    //     Datetime in UTC TMZ
    public static DateTime ToUtcFromBrasiliaTime(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, TimeZoneBrasilia.Id, TimeZoneUtc.Id);
    }

    //
    // Summary:
    //     Convert Brasilia datetime to UTC
    //
    // Parameters:
    //   dateTime:
    //     Datetime in Brasilia
    //
    // Returns:
    //     Datetime in UTC TMZ
    public static DateTime? ToUtcFromBrasiliaTime(this DateTime? dateTime)
    {
        return dateTime?.ToUtcFromBrasiliaTime();
    }
}