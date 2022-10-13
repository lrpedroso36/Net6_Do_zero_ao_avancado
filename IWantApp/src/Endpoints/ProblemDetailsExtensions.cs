using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications.GroupBy(x => x.Key)
                            .ToDictionary(x => x.Key, g => g.Select(y => y.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> identityErrors)
        => identityErrors.GroupBy(x => x.Code)
                         .ToDictionary(x => x.Key, g => g.Select(y => y.Description).ToArray());
}
