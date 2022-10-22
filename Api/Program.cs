using Api.Persistence;
using Api.Persistence.Repositories;
using Api.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(SellerProfile));
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
