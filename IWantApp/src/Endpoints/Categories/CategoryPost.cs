using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Tamplate => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = categoryRequest.Name,
            CreatedBy = "USUARIO.INCLUSAO",
            CreatedOn = DateTime.Now,
            EditedBy = "USUARIO.INCLUSAO",
            EditedOn = DateTime.Now
        };

        context.Categories.Add(category);
        context.SaveChanges();
        
        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
