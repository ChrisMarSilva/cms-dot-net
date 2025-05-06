using System.Globalization;

namespace Cache.Shared.Extensions;

public static class StringExtension
{
    public static decimal ToDecimal(this string value)
    {
        return decimal.Parse(value);
    }

    public static decimal ToDecimal(this string value, NumberStyles style, CultureInfo cultureInfo = null)
    {
        return decimal.Parse(value, style, cultureInfo);
    }

    public static decimal? ToNullableDecimal(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToDecimal();
        }

        return null;
    }

    public static decimal? ToNullabelDecimal(this string value, NumberStyles style, CultureInfo cultureInfo = null)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToDecimal(style, cultureInfo);
        }

        return null;
    }

    public static int ToInt(this string value)
    {
        return int.Parse(value);
    }

    public static int? ToNullableInt(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToInt();
        }

        return null;
    }

    public static uint ToUInt(this string value)
    {
        return uint.Parse(value);
    }

    public static uint? ToNullableUInt(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToUInt();
        }

        return null;
    }

    public static long ToLong(this string value)
    {
        return long.Parse(value);
    }

    public static long? ToNullableLong(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToLong();
        }

        return null;
    }

    public static ulong ToULong(this string value)
    {
        return ulong.Parse(value);
    }

    public static ulong? ToNullableULong(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.ToULong();
        }

        return null;
    }

    public static string Truncate(this string value, int limit)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value.Substring(0, Math.Min(value.Length, limit));
        }

        return value;
    }

    public static string ToNullableString(this string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }

        return null;
    }

    public static string OnlyDigits(this string value)
    {
        return string.Create(value.Length, value, delegate (Span<char> strContent, string valueStr)
        {
            int num = 0;
            for (int i = 0; i < valueStr.Length; i++)
            {
                if (char.IsDigit(valueStr[i]))
                {
                    strContent[num++] = valueStr[i];
                }
            }
        }).TrimEnd('\0');
    }

    public static string OnlyAlphanumeric(this string value)
    {
        return string.Create(value.Length, value, delegate (Span<char> strContent, string valueStr)
        {
            int num = 0;
            foreach (char c in valueStr)
            {
                bool flag;
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                        flag = true;
                        break;
                    default:
                        flag = false;
                        break;
                }

                if (flag)
                {
                    strContent[num++] = c;
                }
            }
        }).TrimEnd('\0');
    }
}