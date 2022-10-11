using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Tamplate => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action([FromHeader] Guid Id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Categories.Where(x => x.Id == Id).FirstOrDefault();

        if (category == null)
        {
            return Results.NotFound();
        }

        category.Update(categoryRequest.Name, categoryRequest.Active, "USUARIO.ALTERACAO");

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemaDetails());
        }

        context.SaveChanges();

        return Results.Ok();
    }
}
