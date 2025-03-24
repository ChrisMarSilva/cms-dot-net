using System.Text;

namespace Cache.Api.Shared.Helpers;

public static class StringHelper
{
    public static string? Read(this HttpRequest request, string headerName, string prefix)
    {
        var stringValues = request.Headers[headerName];
        
        if (stringValues.Count > 0)
            if (!string.IsNullOrWhiteSpace(stringValues[0]))
                return Format(request.Method, request.Path.Value, stringValues[0], prefix);

        return null;
    }

    public static string Format(ReadOnlySpan<char> method, ReadOnlySpan<char> path, ReadOnlySpan<char> key, string prefix)
    {
        var sb = new StringBuilder();

        sb.Append(prefix);
        sb.Append('[');
        sb.Append(method);
        sb.Append(']');
        sb.Append(' ');
        sb.Append(path);
        sb.Append(' ');
        sb.Append('-');
        sb.Append(' ');
        sb.Append(key);

        return sb.ToString();
    }
}
