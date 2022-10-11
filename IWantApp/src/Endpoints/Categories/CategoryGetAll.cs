namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Tamplate => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();

        if (categories == null || !categories.Any())
        {
            return Results.NotFound();
        }

        var caregoriesResponse = categories.Select(x => new CategoryResponse() { Id = x.Id, Name = x.Name, Active = x.Active });

        return Results.Ok(caregoriesResponse);
    }
}
