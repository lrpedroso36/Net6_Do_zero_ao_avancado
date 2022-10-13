using IWantApp.Endpoints.Categories;
using IWantApp.Endpoints.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:IWantApp"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<QueryAllUsersWithClaimName>();

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

app.MapMethods(EmployeePost.Tamplate, EmployeePost.Methods, EmployeePost.Handler);
app.MapMethods(EmployeeGetAll.Tamplate, EmployeeGetAll.Methods, EmployeeGetAll.Handler);

app.MapMethods(TokenPost.Tamplate, TokenPost.Methods, TokenPost.Handler);

app.Run();
