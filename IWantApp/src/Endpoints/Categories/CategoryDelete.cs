using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryDelete
{
    public static string Tamplate => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action([FromHeader] Guid Id, ApplicationDbContext context)
    {
        var category = context.Categories.Where(x => x.Id == Id).FirstOrDefault();

        if (category == null)
        {
            return Results.NotFound();
        }

        context.Remove(category);
        context.SaveChanges();

        return Results.NoContent();
    }
}
