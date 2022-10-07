using IWantApp.Endpoints.Categories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:IWantApp"]);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryPost.Tamplate, CategoryPost.Methods, CategoryPost.Handler);
app.MapMethods(CategoryGetAll.Tamplate, CategoryGetAll.Methods, CategoryGetAll.Handler);
app.MapMethods(CategoryPut.Tamplate, CategoryPut.Methods, CategoryPut.Handler);
app.MapMethods(CategoryDelete.Tamplate, CategoryDelete.Methods, CategoryDelete.Handler);

app.Run();
