namespace IWantApp.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        // return Results.BadRequest(category.Notifications);
        //var errors = category.Notifications.GroupBy(g => g.Key).ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
        //return Results.ValidationProblem(errors);
        return notifications
            .GroupBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {
        //return Results.BadRequest(userResult.Errors.First());
        //return Results.BadRequest(claimsResult.Errors.First());
        //return error.GroupBy(g => g.Code).ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());
        return dictionary;
    }
}
