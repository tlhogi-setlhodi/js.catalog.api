using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;

var builder = WebApplication.CreateBuilder(args);

//Register DbContext
// Add services to the container.
builder.Services.AddDbContext<ThreadAndDaringStoreContext>(Options => Options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

//Register your services here
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CartItemsService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<UserService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
