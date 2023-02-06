using Flunt.Notifications;

namespace IWantApp.Endpoints;

public static class ProblemDetailsExtensions
{

    // return Results.BadRequest(category.Notifications);
    //var errors = category
    //    .Notifications
    //    .GroupBy(g => g.Key)
    //    .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    //return Results.ValidationProblem(errors);

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    //public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    //{
    //    var dictionary = new Dictionary<string, string[]>();
    //    dictionary.Add("Error", error.Select(e => e.Description).ToArray());
    //    return dictionary;
    //}

}
