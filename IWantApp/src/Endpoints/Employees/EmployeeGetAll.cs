using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Tamplate => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(QueryAllUsersWithClaimName queryAllUsersWithClaimName, int page = 1, int rows = 10)
    {
        var result = queryAllUsersWithClaimName.Execute(page, rows);

        if (result == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(result);
    }
}
