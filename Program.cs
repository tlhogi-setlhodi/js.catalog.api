using Microsoft.EntityFrameworkCore;
using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(); // Allow serving images and other static content

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
