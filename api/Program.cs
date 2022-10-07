using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);
var app = builder.Build();

app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) =>
{
    var category = context.Categories.First(x => x.Id == productRequest.categoryId);
    var product = new Product()
    {
        Code = productRequest.code,
        Name = productRequest.name,
        Description = productRequest.description,
        Category = category
    };

    if (productRequest.tags != null)
    {
        product.Tag = new List<Tag>();
        foreach (var item in productRequest.tags)
        {
            product.Tag.Add(new Tag() { Name = item });
        }
    }

    context.Products.Add(product);
    context.SaveChanges();
});

app.MapGet("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>
{
    var product = context.Products
                         .Include(x => x.Tag)
                         .Where(x => x.Id == id).FirstOrDefault();

    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

app.MapPut("/products/{id}", ([FromRoute] int id, ProductRequest productRequest, ApplicationDbContext context) =>
{
    var product = context.Products
                         .Include(x => x.Category)
                         .Include(x => x.Tag)
                         .Where(x => x.Id == id).FirstOrDefault();

    if (product == null)
    {
        return Results.NotFound();
    }

    var category = context.Categories.First(x => x.Id == productRequest.categoryId);

    product.Code = productRequest.code;
    product.Name = productRequest.name;
    product.Description = productRequest.description;
    product.Category = category;
    product.Tag = new List<Tag>();

    if (productRequest.tags != null)
    {
        product.Tag = new List<Tag>();
        foreach (var item in productRequest.tags)
        {
            product.Tag.Add(new Tag() { Name = item });
        }
    }

    context.SaveChanges();
    return Results.Ok();
});

app.MapDelete("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>
{
    var product = context.Products.FirstOrDefault(x => x.Id == id);

    if (product == null)
    {
        return Results.NotFound();
    }

    context.Products.Remove(product);
    context.SaveChanges();

    return Results.NoContent();
});

app.Run();
