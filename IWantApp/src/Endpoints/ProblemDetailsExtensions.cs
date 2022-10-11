using Flunt.Notifications;

namespace IWantApp.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemaDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications.GroupBy(x => x.Key)
                            .ToDictionary(x => x.Key, g => g.Select(y => y.Message).ToArray());
    }
}
