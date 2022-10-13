using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;
public class EmployeePost
{
    public static string Tamplate => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser() { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var claims = new List<Claim>
        {
            new Claim("Name", employeeRequest.Name),
            new Claim("EmployeeCode", employeeRequest.EmployeeCode)
        };

        var claim = userManager.AddClaimsAsync(user, claims).Result;

        if (!claim.Succeeded)
        {
            return Results.ValidationProblem(claim.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
}
